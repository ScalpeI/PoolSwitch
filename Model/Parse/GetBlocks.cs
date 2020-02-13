using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PoolSwitch.DataBase.Concrete;
using PoolSwitch.Model.Work;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PoolSwitch.Model.Parse
{
    class GetBlocks
    {
        public bool Initialized { get; set; }
        private int minheight { get; set; }
        public GetBlocks()
        {
            Initialized = false;
        }
        private async Task<List<string>> GetHeight(int minheight)
        {
            List<string> id = new List<string>();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var req = WebRequest.Create(@"https://blockchain.info/q/getblockcount");
            var r = await req.GetResponseAsync();

            StreamReader responseReader = new StreamReader(r.GetResponseStream());
            var responseData = await responseReader.ReadToEndAsync();
            responseReader.Close();
            int curheight = int.Parse(responseData);
            int range = curheight - minheight;
            if (range != 0)
            {
                for (int i = 0; i < range / 10; i++)
                {
                    int min = minheight + (i * 10) + 1;
                    int max = minheight + ((i + 1) * 10);
                    string ID = String.Format("({0}..{1})", min, max);
                    id.Add(ID);
                }
                if (range % 10 != 0)
                {
                    int min = curheight - (range % 10) + 1;
                    int max = curheight;
                    string ID = String.Format("({0}..{1})", min, max);
                    id.Add(ID);
                }
            }
            else if (range == 1) id.Add(String.Format("({0})", curheight.ToString()));
            return id;
        }
        public async void Get()
        {
            try
            {
                Initialized = true;
                Console.WriteLine("{0} : Launch checking the relevance of the database blocks.", DateTime.Now);
                EFBlockRepository blockRepository = new EFBlockRepository();
                try
                {
                    minheight = blockRepository.Blocks.OrderBy(x => x.height).Select(x => x.height).LastOrDefault();
                }
                catch
                {
                }
                List<string> id = await GetHeight(minheight);
                if (id.Count != 0)
                {
                    Console.WriteLine("{0} : Starting block loading.", DateTime.Now);
                    foreach (string ID in id)
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        var req = WebRequest.Create(@"https://api.blockchair.com/bitcoin/blocks?q=id" + ID);
                        req.ContentType = "application/json";
                        var r = await req.GetResponseAsync();

                        StreamReader responseReader = new StreamReader(r.GetResponseStream());
                        var responseData = await responseReader.ReadToEndAsync();
                        JObject obj = JObject.Parse(responseData);
                        dynamic jsonDe = JsonConvert.DeserializeObject(obj["data"].ToString());
                        try
                        {
                            if (jsonDe.GetType().ToString() == "Newtonsoft.Json.Linq.JArray")
                            {
                                foreach (JObject typeStr in jsonDe)
                                {
                                    if (blockRepository.Blocks.Where(x => x.height == int.Parse(typeStr["id"].ToString())).Count() == 0)
                                    {
                                        await blockRepository.Create(int.Parse(typeStr["id"].ToString()), (int)DateTime.Parse(typeStr["median_time"].ToString()).Subtract(new DateTime(1970, 1, 1)).TotalSeconds, typeStr["guessed_miner"].ToString());
                                        Console.WriteLine("{1} : Add Block Height {0}.", typeStr["id"].ToString(), DateTime.Now);
                                    }
                                }
                            }
                        }
                        catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("{0} : The base of blocks is relevant.", DateTime.Now);
                }
                Console.WriteLine("{0} : Stop checking the relevance of the block database.", DateTime.Now);

                FoundBlock foundBlock = new FoundBlock();
                foundBlock.SwitchPool();
                Initialized = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
