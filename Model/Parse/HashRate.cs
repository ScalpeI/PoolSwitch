using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PoolSwitch.Model.Parse
{
    public class HashRate
    {
        public async Task<string> Rate()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var req = WebRequest.Create(@"https://api.blockchain.info/charts/hash-rate?format=json");
            var r = await req.GetResponseAsync();

            StreamReader responseReader = new StreamReader(r.GetResponseStream());
            var responseData = await responseReader.ReadToEndAsync();
            JObject obj = JObject.Parse(responseData);
            dynamic jsonDe = JsonConvert.DeserializeObject(obj["values"].ToString());
            string hash = "";
            foreach (JObject once in jsonDe)
            {
                hash=once["y"].ToString();
            }
            //Console.WriteLine(hash);
            return hash;
        }
    }
}
