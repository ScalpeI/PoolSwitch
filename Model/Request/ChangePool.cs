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
    public class ChangePool
    {
        public async Task<dynamic> GetResponsePool(string Mkey, string Msecret, string id)
        {

            hash_hmac hmac = new hash_hmac();
            string ID = id;
            string Key = Mkey;
            string Secret = Msecret;
            double mtime = Math.Round((DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds * 10000);
            string endpoint = "/rig/"+ ID + "/pool";
            //string endpoint = "/rig/141972/pool";
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

        public async Task<string> PutResponsePool(string Mkey, string Msecret, string id, string fields)
        {
            hash_hmac hmac = new hash_hmac();
            string ID = id;
            string Key = Mkey;
            string Secret = Msecret;
            double mtime = Math.Round((DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds * 10000);
            string endpoint = "/rig/"+ ID + "/pool";
            //string endpoint = "/rig/141972/pool";
            string sign_string = Key + mtime.ToString() + endpoint;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var req = WebRequest.Create(@"https://www.miningrigrentals.com/api/v2" + endpoint + fields);
            //Console.WriteLine(req.RequestUri);
            req.Headers.Add("x-api-sign:" + hmac.sha1(sign_string, Secret));
            req.Headers.Add("x-api-key:" + Key);
            req.Headers.Add("x-api-nonce:" + mtime.ToString());
            req.Method = "PUT";
            req.ContentType = "application/x-www-form-urlencoded";
            WebResponse r = await req.GetResponseAsync();
            StreamReader responseReader = new StreamReader(r.GetResponseStream());
            //Console.WriteLine(req.RequestUri.AbsoluteUri);
            //Console.WriteLine(await responseReader.ReadToEndAsync());
            return await responseReader.ReadToEndAsync();
        }
        public async void Change(string namepool)
        {
            EFUserRepository userRep = new EFUserRepository();
            foreach (var useronce in userRep.Users)
            {
                if (useronce.Mkey != null && useronce.Msecret != null )
                {
                    if (useronce.Login == "test"  || useronce.Login == "schafridrich") 
                    {
                        try
                        {
                            var responseDataRig = "";
                            string check = "False";
                            while (check == "False")
                            {
                                responseDataRig = await GetResponseRig(useronce.Mkey, useronce.Msecret);
                                check = JObject.Parse(responseDataRig)["success"].ToString();
                            }

                            JObject objrig = JObject.Parse(responseDataRig);
                            dynamic jsonDerig = JsonConvert.DeserializeObject(objrig["data"].ToString());
                            string ID = "";
                            foreach (JObject typeStr in jsonDerig)
                            {
                                ID += typeStr["id"].ToString() + ";";
                            }
                            if (!string.IsNullOrEmpty(ID))
                            {
                                var responseData = await GetResponsePool(useronce.Mkey, useronce.Msecret, ID);
                                JObject obj = JObject.Parse(responseData);
                                dynamic jsonDe = JsonConvert.DeserializeObject(obj["data"]["pools"].ToString());
                                //List<string> host = new List<string>();
                                //List<string> port = new List<string>();
                                //List<string> user = new List<string>();
                                //List<string> pass = new List<string>();
                                //List<string> priority = new List<string>();
                                //Host.Host host = new Host.Host();
                                List<Host.Host> host = new List<Host.Host>();
                                foreach (JObject typeStr in jsonDe)
                                {
                                    if (typeStr["host"].ToString() == "eu.ss.btc.com")
                                    {
                                        host.Add(
                                            new Host.Host
                                            {
                                                poolname = "BTC",
                                                host = typeStr["host"].ToString(),
                                                port = typeStr["port"].ToString(),
                                                user = typeStr["user"].ToString(),
                                                pass = typeStr["pass"].ToString(),
                                                priority = typeStr["priority"].ToString()
                                            });
                                    }
                                    else if (typeStr["host"].ToString() == "stratum.slushpool.com")
                                    {
                                        host.Add(
                                            new Host.Host
                                            {
                                                poolname = "SlushPool",
                                                host = typeStr["host"].ToString(),
                                                port = typeStr["port"].ToString(),
                                                user = typeStr["user"].ToString(),
                                                pass = typeStr["pass"].ToString(),
                                                priority = typeStr["priority"].ToString()
                                            });
                                    }
                                    else if (typeStr["host"].ToString() == "btc.viabtc.com")
                                    {
                                        host.Add(
                                            new Host.Host
                                            {
                                                poolname = "ViaBTC",
                                                host = typeStr["host"].ToString(),
                                                port = typeStr["port"].ToString(),
                                                user = typeStr["user"].ToString(),
                                                pass = typeStr["pass"].ToString(),
                                                priority = typeStr["priority"].ToString()
                                            });
                                    }
                                    //host.Add(typeStr["host"].ToString());
                                    //port.Add(typeStr["port"].ToString());
                                    //user.Add(typeStr["user"].ToString());
                                    //pass.Add(typeStr["pass"].ToString());
                                    //priority.Add(typeStr["priority"].ToString());
                                }
                                string fields;
                                switch (namepool)
                                {
                                    case "ViaBTC":
                                        Console.WriteLine("Pool {4}\nhost : {0}\nport : {1}\nuser : {2}\npass : {3}\npriority : {5}\n",
                                           host.Where(x => x.poolname == "ViaBTC").Select(x => x.host).FirstOrDefault(),
                                           host.Where(x => x.poolname == "ViaBTC").Select(x => x.port).FirstOrDefault(),
                                           host.Where(x => x.poolname == "ViaBTC").Select(x => x.user).FirstOrDefault(),
                                           host.Where(x => x.poolname == "ViaBTC").Select(x => x.pass).FirstOrDefault(),
                                           host.Where(x => x.poolname == "ViaBTC").Select(x => x.poolname).FirstOrDefault(),
                                        host.Where(x => x.poolname == "ViaBTC").Select(x => x.priority).FirstOrDefault());


                                        fields = String.Format("?host={0}&port={1}&user={2}&pass={3}&priority={4}",
                                            host.Where(x => x.poolname == "ViaBTC").Select(x => x.host).FirstOrDefault(),
                                            host.Where(x => x.poolname == "ViaBTC").Select(x => x.port).FirstOrDefault(),
                                            host.Where(x => x.poolname == "ViaBTC").Select(x => x.user).FirstOrDefault(),
                                            host.Where(x => x.poolname == "ViaBTC").Select(x => x.pass).FirstOrDefault(),
                                            0);
                                        string check1 = "False";
                                        while (check1 == "False")
                                        {
                                            responseData = await PutResponsePool(useronce.Mkey, useronce.Msecret, ID, fields);
                                            check1 = JProperty.Parse(responseData)["success"].ToString();
                                            Console.WriteLine("ViaBTC {0}", check1);
                                        }



                                        fields = String.Format("?host={0}&port={1}&user={2}&pass={3}&priority={4}",
                                            host.Where(x => x.poolname == "BTC").Select(x => x.host).FirstOrDefault(),
                                            host.Where(x => x.poolname == "BTC").Select(x => x.port).FirstOrDefault(),
                                            host.Where(x => x.poolname == "BTC").Select(x => x.user).FirstOrDefault(),
                                            host.Where(x => x.poolname == "BTC").Select(x => x.pass).FirstOrDefault(),
                                            1);
                                        string check2 = "False";
                                        while (check2 == "False")
                                        {
                                            responseData = await PutResponsePool(useronce.Mkey, useronce.Msecret, ID, fields);
                                            check2 = JProperty.Parse(responseData)["success"].ToString();
                                            Console.WriteLine("BTC {0}", check2);
                                        }


                                        fields = String.Format("?host={0}&port={1}&user={2}&pass={3}&priority={4}",
                                            host.Where(x => x.poolname == "SlushPool").Select(x => x.host).FirstOrDefault(),
                                            host.Where(x => x.poolname == "SlushPool").Select(x => x.port).FirstOrDefault(),
                                            host.Where(x => x.poolname == "SlushPool").Select(x => x.user).FirstOrDefault(),
                                            host.Where(x => x.poolname == "SlushPool").Select(x => x.pass).FirstOrDefault(),
                                            2);
                                        string check3 = "False";
                                        while (check3 == "False")
                                        {
                                            responseData = await PutResponsePool(useronce.Mkey, useronce.Msecret, ID, fields);
                                            check3 = JProperty.Parse(responseData)["success"].ToString();
                                            Console.WriteLine("SlushPool {0}", check3);
                                        }
                                        break;
                                    case "SlushPool":
                                        Console.WriteLine("Pool {4}\nhost : {0}\nport : {1}\nuser : {2}\npass : {3}\npriority : {5}\n",
                                        host.Where(x => x.poolname == "SlushPool").Select(x => x.host).FirstOrDefault(),
                                        host.Where(x => x.poolname == "SlushPool").Select(x => x.port).FirstOrDefault(),
                                        host.Where(x => x.poolname == "SlushPool").Select(x => x.user).FirstOrDefault(),
                                        host.Where(x => x.poolname == "SlushPool").Select(x => x.pass).FirstOrDefault(),
                                           host.Where(x => x.poolname == "SlushPool").Select(x => x.poolname).FirstOrDefault(),
                                        host.Where(x => x.poolname == "SlushPool").Select(x => x.priority).FirstOrDefault());


                                        fields = String.Format("?host={0}&port={1}&user={2}&pass={3}&priority={4}",
                                            host.Where(x => x.poolname == "SlushPool").Select(x => x.host).FirstOrDefault(),
                                            host.Where(x => x.poolname == "SlushPool").Select(x => x.port).FirstOrDefault(),
                                            host.Where(x => x.poolname == "SlushPool").Select(x => x.user).FirstOrDefault(),
                                            host.Where(x => x.poolname == "SlushPool").Select(x => x.pass).FirstOrDefault(),
                                            0);
                                        string check4 = "False";
                                        while (check4 == "False")
                                        {
                                            responseData = await PutResponsePool(useronce.Mkey, useronce.Msecret, ID, fields);
                                            check4 = JProperty.Parse(responseData)["success"].ToString();
                                            Console.WriteLine("SlushPool {0}", check4);
                                        }


                                        fields = String.Format("?host={0}&port={1}&user={2}&pass={3}&priority={4}",
                                            host.Where(x => x.poolname == "BTC").Select(x => x.host).FirstOrDefault(),
                                            host.Where(x => x.poolname == "BTC").Select(x => x.port).FirstOrDefault(),
                                            host.Where(x => x.poolname == "BTC").Select(x => x.user).FirstOrDefault(),
                                            host.Where(x => x.poolname == "BTC").Select(x => x.pass).FirstOrDefault(),
                                            1);
                                        string check5 = "False";
                                        while (check5 == "False")
                                        {
                                            responseData = await PutResponsePool(useronce.Mkey, useronce.Msecret, ID, fields);
                                            check5 = JProperty.Parse(responseData)["success"].ToString();
                                            Console.WriteLine("BTC {0}", check5);
                                        }


                                        fields = String.Format("?host={0}&port={1}&user={2}&pass={3}&priority={4}",
                                            host.Where(x => x.poolname == "ViaBTC").Select(x => x.host).FirstOrDefault(),
                                            host.Where(x => x.poolname == "ViaBTC").Select(x => x.port).FirstOrDefault(),
                                            host.Where(x => x.poolname == "ViaBTC").Select(x => x.user).FirstOrDefault(),
                                            host.Where(x => x.poolname == "ViaBTC").Select(x => x.pass).FirstOrDefault(),
                                            2);
                                        string check6 = "False";
                                        while (check6 == "False")
                                        {
                                            responseData = await PutResponsePool(useronce.Mkey, useronce.Msecret, ID, fields);
                                            check6 = JProperty.Parse(responseData)["success"].ToString();
                                            Console.WriteLine("ViaBTC {0}", check6);
                                        }

                                        break;
                                    default:
                                        Console.WriteLine("Pool {4}\nhost : {0}\nport : {1}\nuser : {2}\npass : {3}\npriority : {5}\n",
                                        host.Where(x => x.poolname == "BTC").Select(x => x.host).FirstOrDefault(),
                                        host.Where(x => x.poolname == "BTC").Select(x => x.port).FirstOrDefault(),
                                        host.Where(x => x.poolname == "BTC").Select(x => x.user).FirstOrDefault(),
                                        host.Where(x => x.poolname == "BTC").Select(x => x.pass).FirstOrDefault(),
                                        host.Where(x => x.poolname == "BTC").Select(x => x.poolname).FirstOrDefault(),
                                        host.Where(x => x.poolname == "BTC").Select(x => x.priority).FirstOrDefault());


                                        fields = String.Format("?host={0}&port={1}&user={2}&pass={3}&priority={4}",
                                            host.Where(x => x.poolname == "BTC").Select(x => x.host).FirstOrDefault(),
                                            host.Where(x => x.poolname == "BTC").Select(x => x.port).FirstOrDefault(),
                                            host.Where(x => x.poolname == "BTC").Select(x => x.user).FirstOrDefault(),
                                            host.Where(x => x.poolname == "BTC").Select(x => x.pass).FirstOrDefault(),
                                            0);
                                        string check7 = "False";
                                        while (check7 == "False")
                                        {
                                            responseData = await PutResponsePool(useronce.Mkey, useronce.Msecret, ID, fields);
                                            check7 = JProperty.Parse(responseData)["success"].ToString();
                                            Console.WriteLine("BTC {0}", check7);
                                        }


                                        fields = String.Format("?host={0}&port={1}&user={2}&pass={3}&priority={4}",
                                            host.Where(x => x.poolname == "ViaBTC").Select(x => x.host).FirstOrDefault(),
                                            host.Where(x => x.poolname == "ViaBTC").Select(x => x.port).FirstOrDefault(),
                                            host.Where(x => x.poolname == "ViaBTC").Select(x => x.user).FirstOrDefault(),
                                            host.Where(x => x.poolname == "ViaBTC").Select(x => x.pass).FirstOrDefault(),
                                            1);
                                        string check8 = "False";
                                        while (check8 == "False")
                                        {
                                            responseData = await PutResponsePool(useronce.Mkey, useronce.Msecret, ID, fields);
                                            check8 = JProperty.Parse(responseData)["success"].ToString();
                                            Console.WriteLine("ViaBTC {0}", check8);
                                        }


                                        fields = String.Format("?host={0}&port={1}&user={2}&pass={3}&priority={4}",
                                            host.Where(x => x.poolname == "SlushPool").Select(x => x.host).FirstOrDefault(),
                                            host.Where(x => x.poolname == "SlushPool").Select(x => x.port).FirstOrDefault(),
                                            host.Where(x => x.poolname == "SlushPool").Select(x => x.user).FirstOrDefault(),
                                            host.Where(x => x.poolname == "SlushPool").Select(x => x.pass).FirstOrDefault(),
                                            2);
                                        string check9 = "False";
                                        while (check9 == "False")
                                        {
                                            responseData = await PutResponsePool(useronce.Mkey, useronce.Msecret, ID, fields);
                                            check9 = JProperty.Parse(responseData)["success"].ToString();
                                            Console.WriteLine("SlushPool {0}", check9);
                                        }
                                        break;
                                }

                                //for (int i = 0; i < host.Count; i++)
                                //{

                                //    Console.WriteLine("Pool №{5}\nhost : {0}\nport : {1}\nuser : {2}\npass : {3}\npriority : {4}\n",
                                //        host.ElementAt(i), port.ElementAt(i), user.ElementAt(i), pass.ElementAt(i), priority.ElementAt(i), i + 1);

                                //    string fields = String.Format("?host={0}&port={1}&user={2}&pass={3}&priority={4}",
                                //        host.ElementAt(i), port.ElementAt(i), user.ElementAt(i), pass.ElementAt(i), priority.ElementAt(host.Count - 1 - i));
                                //    //Console.WriteLine(await PutResponsePool(useronce.Mkey, useronce.Msecret, "1", fields));
                                //}

                            }
                        }
                        catch (Exception ex) { Console.WriteLine(ex); }
                    }
                }
            }
        }
    }
}
