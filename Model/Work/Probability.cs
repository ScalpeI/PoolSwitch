using PoolSwitch.DataBase.Concrete;
using PoolSwitch.DataBase.Entities;
using PoolSwitch.Model.Parse;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Pool = PoolSwitch.Model.Parse.Pool;
using PoolSwitch.Model.Work.Function;

namespace PoolSwitch.Model.Work
{
    class Probability
    {
        private BigInteger Factorial(int n)
        {
            int p = 0, c = 0;
            while ((n >> p) > 1)
            {
                p++; c += n >> p;
            }
            BigInteger r = 1;
            int k = 1;
            for (; p >= 0; p--)
            {
                int n1 = n >> p;
                BigInteger x = 1;
                int a = 1;
                for (; k <= n1; k += 2)
                {
                    if ((long)a * k < int.MaxValue) a *= k;
                    else
                    {
                        x *= a; a = k;
                    }
                }
                r *= BigInteger.Pow(x * a, p + 1);
            }

            return r << c;
        }
        private double Calc(double networkhrt, double poolhrt, int countblock, int forecastblock, int findblock, int countfindblock)
        {
            //A
            double A = poolhrt / networkhrt;
            //Console.WriteLine("A:{0}", A);
            //N
            int allblock = countblock + forecastblock;
            //Console.WriteLine("allblock:{0}", allblock);
            //M
            double M = A * allblock;
            //Console.WriteLine("M:{0}", M);
            //N-M
            double NM = allblock - M;
            //Console.WriteLine("NM:{0}", NM);
            //n=forecastblock
            //m=findblock
            double P = 0;
            for (int i = findblock; i <= M; i++)
            {
                //n-m
                int nm = forecastblock - i;
                //Console.WriteLine("nm:{0}", nm);
                //C m M
                BigInteger CmM = Factorial((int)Math.Round(M)) / (Factorial(i) * Factorial((int)Math.Round(M - i)));
                //Console.WriteLine("CmM:{0}", CmM);
                //C n-m N-M
                BigInteger CnmNM = Factorial((int)Math.Round(NM)) / (Factorial(nm) * Factorial((int)Math.Round(NM - nm)));
                //Console.WriteLine("CnmNM:{0}", CnmNM);
                //C n N
                BigInteger CnN = Factorial(allblock) / (Factorial(forecastblock) * Factorial(allblock - forecastblock));
                //Console.WriteLine("CnN:{0}", CnN);
                //P
                P += (double)((CmM * CnmNM) * 10000 / CnN);
            }
            return P / 100;
        }
        public async void CalculateProbability(string namepool)//Task<List<double>> CalculateProbability(string namepool,int countblock, int forecastblock)
        {
            int countblock = 1000;
            int forecastblock = 100;
            HashRate hashRate = new HashRate();
            EFMinearRepository eFMinear = new EFMinearRepository();
            //network hrt
            float hrn = float.Parse(await hashRate.Rate()) / 1000;

            Pool pool = new Pool();

            //pool hrt
            string hashs = pool.Parse(namepool);
            //string hashs = pool.Parse("ViaBTC");
            hashs = hashs.Substring(0, hashs.Length - 5).Replace(",", "").Replace(".", ",");
            double hash = double.Parse(hashs);

            //T
            double T = forecastblock * 10 / 60;
            //double T = 100 * 10 / 60;

            //BTC.com
            double btc = (T / 24) * 1.6 * 1000 * double.Parse(eFMinear.Minears.OrderBy(x => x.date).Select(x => x.fpps_mining_earnings).LastOrDefault(), CultureInfo.InvariantCulture);
            int countfb = (int)Math.Ceiling(btc / 1.6 * hash / 12.8);
            //Console.WriteLine("Необходимое количество найденных блоков для прибыли : {0} ", countfb);
            EFBlockRepository eFBlock = new EFBlockRepository();
            IEnumerable<Block> countb = eFBlock.Blocks.OrderByDescending(x => x.height).Take(countblock);
            int count = countb.Where(x => x.poolname == namepool).Count();
            //IEnumerable<Block> countb = eFBlock.Blocks.OrderByDescending(x => x.height).Take(1000);
            //int count = countb.Where(x => x.poolname == "ViaBTC").Count();

            //P
            double P = Calc(hrn, hash, countblock, forecastblock, countfb, count);
            //double P = Calc(hrn, hash, 1000, 100, countfb, count);
            Console.WriteLine("{3} : Вероятность {1} для {2} и более блоков за {4} блоков : {0}%", P, namepool, countfb, DateTime.Now, forecastblock);
            //Console.WriteLine("Вероятность {1} : {0}%", P, namepool);

            //Revenue
            //double revenue1 = findblock * 1.6 / viabtchash * 12.8;
            //double revenue2 = findblock * 1.6 / slushpoolhash * 12.8;
            double revenue = (countfb * 1.6 / hash * 12.8) - btc;
            //Console.WriteLine("Доход {1} : {0} BTC", revenue, "ViaBTC");
            Console.WriteLine("{3} : Доход {1} при {2} блоков : {0} BTC", revenue, namepool, countfb, DateTime.Now);
            //List<double> ret = new List<double> { P, revenue };
            //return ret;


            //laplas
            double matwait = (hash / hrn) * 144;
            int maxheight = eFBlock.Blocks.OrderByDescending(x => x.height).Select(x=>x.height).FirstOrDefault();
            IEnumerable<Block> countbl = eFBlock.Blocks.OrderByDescending(x => x.height).Take(countblock);
            List <int> cntbl = new List<int>();
            cntbl.Add(countbl.Where(x => x.height <= maxheight && x.height >= maxheight - 143 && x.poolname == namepool).Count());
            cntbl.Add(countbl.Where(x => x.height < maxheight-143 && x.height >= maxheight - 287 && x.poolname == namepool).Count());
            cntbl.Add(countbl.Where(x => x.height < maxheight-287 && x.height >= maxheight - 431 && x.poolname == namepool).Count());
            cntbl.Add(countbl.Where(x => x.height < maxheight-431 && x.height >= maxheight - 575 && x.poolname == namepool).Count());
            cntbl.Add(countbl.Where(x => x.height < maxheight-575 && x.height >= maxheight - 719 && x.poolname == namepool).Count());
            Console.WriteLine("{3} : Вероятность {1} для {2} и более блоков : {0}%", Function.Function.FuncLaplas(cntbl, matwait, countfb)*100, namepool, countfb, DateTime.Now);
        }
    }
}
