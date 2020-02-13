using PoolSwitch.DataBase.Concrete;
using PoolSwitch.DataBase.Entities;
using PoolSwitch.Model.Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pool = PoolSwitch.Model.Parse.Pool;

namespace PoolSwitch.Model.Work
{
    public class Luck
    {
        public async Task<List<float>> CalculateLuck(int countblock)
        {
            HashRate hashRate = new HashRate();
            float hrn = float.Parse(await hashRate.Rate())/1000;
            Pool pool = new Pool();
            string viabtc = "ViaBTC";
            string slushpool = "SlushPool";
            string viabtchash = pool.Parse(viabtc);
            string slushpoolhash = pool.Parse(slushpool);
            viabtchash = viabtchash.Substring(0, viabtchash.Length - 5).Replace(",","").Replace(".",",");
            slushpoolhash = slushpoolhash.Substring(0, slushpoolhash.Length - 5).Replace(",", "").Replace(".", ",");
            //Console.WriteLine(viabtchash);
            float viabtcpie = ((( float.Parse(viabtchash)*100 / hrn)) * countblock)/100;
            float slushpoolpie = (((float.Parse(slushpoolhash)*100 / hrn)) * countblock)/ 100;
            EFBlockRepository eFBlock = new EFBlockRepository();
            IEnumerable<Block> countb = eFBlock.Blocks.OrderByDescending(x => x.height).Take(countblock);
            int countviabtc = countb.Where(x=>x.poolname== viabtc).Count();
            int countslushpool = countb.Where(x => x.poolname == slushpool).Count();
            float luckviabtc = 100 * countviabtc / viabtcpie;
            float luckslushpool = 100 * countslushpool / slushpoolpie;
            Console.WriteLine(luckviabtc);
            Console.WriteLine(luckslushpool);
            List<float> luck = new List<float>() { luckviabtc, luckslushpool };
            return luck;
        }
    }
}
