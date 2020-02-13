using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PoolSwitch.DataBase.Concrete;
using PoolSwitch.Model.Hash;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PoolSwitch.Model.Request
{
    public class Mrr
    {
        public async Task<dynamic> GetResponseRig(string Mkey, string Msecret)
        {
            hash_hmac hmac = new hash_hmac();
            string Key = Mkey;
            string Secret = Msecret;
            double mtime = Math.Round((DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds * 10000);
            string endpoint = "/rig/mine";
            string sign_string = Key + mtime.ToString() + endpoint;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var req = WebRequest.Create(@"https://www.miningrigrentals.com/api/v2" + endpoint);
            req.Headers.Add("x-api-sign:" + hmac.sha1(sign_string, Secret));
            req.Headers.Add("x-api-key:" + Key);
            req.Headers.Add("x-api-nonce:" + mtime.ToString());
            var r = await req.GetResponseAsync();
            StreamReader responseReader = new StreamReader(r.GetResponseStream());
            return await responseReader.ReadToEndAsync();
        }
        public async Task<dynamic> GetResponsePool(string Mkey, string Msecret,string id)
        {
            hash_hmac hmac = new hash_hmac();
            string ID = id;
            string Key = Mkey;
            string Secret = Msecret;
            double mtime = Math.Round((DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds * 10000);
            //string endpoint = "/rig/"+ ID + "/pool";
            string endpoint = "/rig/132643/pool";
            string sign_string = Key + mtime.ToString() + endpoint;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var req = WebRequest.Create(@"https://www.miningrigrentals.com/api/v2" + endpoint);
            req.Headers.Add("x-api-sign:" + hmac.sha1(sign_string, Secret));
            req.Headers.Add("x-api-key:" + Key);
            req.Headers.Add("x-api-nonce:" + mtime.ToString());
            var r = await req.GetResponseAsync();
            StreamReader responseReader = new StreamReader(r.GetResponseStream());
            return await responseReader.ReadToEndAsync();
        }
        public async void Upload()
        {
            EFUserRepository user = new EFUserRepository();
            foreach (var useronce in user.Users)
            {
                if (useronce.Mkey != null && useronce.Msecret != null)
                {
                    try
                    {
                        var responseData = "";
                        string check = "False";
                        while (check == "False")
                        {
                            responseData = await GetResponseRig(useronce.Mkey, useronce.Msecret);
                            check = JObject.Parse(responseData)["success"].ToString();
                        }
                        JObject obj = JObject.Parse(responseData);
                        dynamic jsonDe = JsonConvert.DeserializeObject(obj["data"].ToString());
                        string ID = "";
                        foreach (JObject typeStr in jsonDe)
                        {
                            ID += typeStr["id"].ToString() + ";";
                        }
                        //Console.WriteLine(ID);

                        
                        var responseData1 = await GetResponsePool(useronce.Mkey, useronce.Msecret,ID);
                        Console.WriteLine(responseData1);
                    }
                    catch { }
                }
            }
        }
    }
}
