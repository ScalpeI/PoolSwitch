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
using System.Threading;
using System.Threading.Tasks;

namespace PoolSwitch.Model.Parse
{
    public class BlockHeight
    {
        public bool Initialized { get; set; }
        public BlockHeight()
        {
            Initialized = false;
        }
        private async Task<string> GetHeight(int heightmin)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var req = WebRequest.Create(@"https://blockchain.info/q/getblockcount");
                var r = await req.GetResponseAsync();

                StreamReader responseReader = new StreamReader(r.GetResponseStream());
                var responseData = await responseReader.ReadToEndAsync();
                responseReader.Close();
                //JObject obj = JObject.Parse(responseData);
                //dynamic jsonDe = JsonConvert.DeserializeObject(obj["data"].ToString());
                //Console.WriteLine(responseData);
                int height = int.Parse(responseData);
                int heightrange = height - heightmin;
                if (heightrange != 0)
                {
                    IEnumerable<int> heights = Enumerable.Range(heightmin + 1, heightrange);
                    string ID = "";
                    foreach (int num in heights)
                    {
                        ID += num.ToString() + ",";
                    }
                    return ID.Substring(0, ID.Length - 1);
                }
                else return heightmin.ToString();
            }
            catch
            {
                return heightmin.ToString();
            }
        }

        public async void GetBlocks()
        {

            Initialized = true;
            Console.WriteLine("{0} : Launch checking the relevance of the database blocks.", DateTime.Now);
            //DateTime date = DateTime.Parse("20.09.2019");
            //for (DateTime i = date; i < DateTime.Now; i = i.AddDays(1))
            //{
            try
            {
                EFBlockRepository blockRepository = new EFBlockRepository();
                int minheight = blockRepository.Blocks.OrderBy(x => x.height).Select(x => x.height).LastOrDefault();
                string ID = await GetHeight(minheight);
                if (ID != minheight.ToString())
                {
                    Console.WriteLine("{0} : Starting block loading.", DateTime.Now);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    Console.WriteLine("https://chain.api.btc.com/v3/block/" + ID);
                    var req = WebRequest.Create(@"https://chain.api.btc.com/v3/block/" + ID);
                    //var req = WebRequest.Create(@"https://chain.api.btc.com/v3/block/date/" + i.ToString("yyyyMMdd"));
                    req.ContentType = "application/json";
                    var r = await req.GetResponseAsync();

                    StreamReader responseReader = new StreamReader(r.GetResponseStream());
                    var responseData = await responseReader.ReadToEndAsync();
                    JObject obj = JObject.Parse(responseData);
                    dynamic jsonDe = JsonConvert.DeserializeObject(obj["data"].ToString());
                    if (jsonDe.GetType().ToString() == "Newtonsoft.Json.Linq.JArray")
                    {
                        foreach (JObject typeStr in jsonDe)
                        {
                            //Console.WriteLine("Current Height " + typeStr["height"].ToString());
                            if (blockRepository.Blocks.Where(x => x.height == int.Parse(typeStr["height"].ToString())).Count() == 0)
                            {
                                await blockRepository.Create(int.Parse(typeStr["height"].ToString()), int.Parse(typeStr["timestamp"].ToString()), typeStr["extras"]["pool_name"].ToString());
                                Console.WriteLine("{1} : Add Block Height {0}.", typeStr["height"].ToString(), DateTime.Now);
                            }
                        }
                    }
                    else
                    {
                        //Console.WriteLine("Current Height " + jsonDe["height"].ToString());
                        if (blockRepository.Blocks.Where(x => x.height == int.Parse(jsonDe["height"].ToString())).Count() == 0)
                        {
                            await blockRepository.Create(int.Parse(jsonDe["height"].ToString()), int.Parse(jsonDe["timestamp"].ToString()), jsonDe["extras"]["pool_name"].ToString());
                            Console.WriteLine("{1} : Add Block Height {0}.", jsonDe["height"].ToString(), DateTime.Now);
                        }
                    }

                }

                else
                {
                    Console.WriteLine("{0} : The base of blocks is relevant.", DateTime.Now);
                }
                Console.WriteLine("{0} : Stop checking the relevance of the block database.", DateTime.Now);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} : Error block loading", DateTime.Now);
            }
            //Probability probability = new Probability();
            //probability.CalculateProbability("ViaBTC");
            //probability.CalculateProbability("SlushPool");
            FoundBlock foundBlock = new FoundBlock();
            foundBlock.SwitchPool();
            Initialized = false;
            //Thread.Sleep(3000);
            //Task.WaitAll(blockRepository.Create());
            //}
        }
    }
}
