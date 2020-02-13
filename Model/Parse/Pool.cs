using HtmlAgilityPack;
using PoolSwitch.DataBase.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PoolSwitch.Model.Parse
{
    class Pool
    {
        public string Parse(string name)
        {
            EFParseRepository parseRepository = new EFParseRepository();
            Uri address;
            switch (name)
            {
                case "SlushPool":
                    address = new Uri("https://btc.com/stats/pool/SlushPool");
                    break;
                case "ViaBTC":
                    address = new Uri("https://btc.com/stats/pool/ViaBTC");
                    break;
                default:
                    address = new Uri("http://1.com");
                    break;
            }
            string ratepool = "";
            if (address.ToString() != "http://1.com/")
            {
                WebClient web = new WebClient();
                web.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                Stream data = web.OpenRead(address);
                var html = new HtmlDocument();
                html.Load(data);
                var bodies = html.DocumentNode
                    .SelectNodes("//body/div/div/div/div/table/tr/td/table/tr/td/table/tr/td");
                foreach (var tag in bodies)
                {
                    if (tag.Attributes["class"].Value == "text-right")
                    {
                        if (Regex.IsMatch(tag.InnerHtml, ".*H/s"))
                        {
                            //var body = tag.InnerHtml;
                            //Console.WriteLine("{0} - {1}",name, tag.InnerHtml);
                            ratepool = tag.InnerHtml;
                           // await parseRepository.Create(name, tag.InnerHtml);
                        }
                    }
                }
            }
            return ratepool;

        }
    }
}
