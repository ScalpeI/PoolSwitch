using PoolSwitch.DataBase.Concrete;
using PoolSwitch.DataBase.Entities;
using PoolSwitch.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolSwitch.Model.Work
{
    public class FoundBlock
    {
        private bool FoundBlockSuccess = true;
        public static bool TaskChangePool { get; private set; }
        private static int BlockStartWork { get; set; }
        private static string PoolStartWork { get; set; }
        private static readonly List<string> pools = new List<string> { "ViaBTC", "SlushPool" };
        public void SwitchPool()
        {
            if (!TaskChangePool)
            {
                Console.WriteLine("{0} : Starting service CheckCountBlocks.", DateTime.Now);
                Mining.Write(String.Format("{0} : Starting service CheckCountBlocks.", DateTime.Now));
                Console.WriteLine("{0} : FoundBlockSuccess - {1}\nTaskChangePool - {2}\nBlockStartWork - {3}\nPoolStartWork - {4}", 
                    DateTime.Now, FoundBlockSuccess,TaskChangePool, BlockStartWork, PoolStartWork);
                Mining.Write(String.Format("{0} : FoundBlockSuccess - {1}\nTaskChangePool - {2}\nBlockStartWork - {3}\nPoolStartWork - {4}",
                    DateTime.Now, FoundBlockSuccess, TaskChangePool, BlockStartWork, PoolStartWork));
                foreach (string pool in pools)
                {
                    if (FoundBlockSuccess) { FoundBlocks(pool, TaskChangePool); }
                }
                if (FoundBlockSuccess) {
                    Request.ChangePool change = new Request.ChangePool();
                    change.Change("BTC"); return; }
            }
            else
            {
                Console.WriteLine("{0} : Update service CheckCountBlocks.", DateTime.Now);
                Mining.Write(String.Format("{0} : Update service CheckCountBlocks.", DateTime.Now));
                Console.WriteLine("{0} : FoundBlockSuccess - {1}\nTaskChangePool - {2}\nBlockStartWork - {3}\nPoolStartWork - {4}",
                    DateTime.Now, FoundBlockSuccess, TaskChangePool, BlockStartWork, PoolStartWork);
                Mining.Write(String.Format("{0} : FoundBlockSuccess - {1}\nTaskChangePool - {2}\nBlockStartWork - {3}\nPoolStartWork - {4}",
                    DateTime.Now, FoundBlockSuccess, TaskChangePool, BlockStartWork, PoolStartWork));
                FoundBlocks(PoolStartWork, TaskChangePool);
            }
        }
        private void FoundBlocks(string namepool, bool task)
        {
            if (!task)
            {
                Console.WriteLine("{0} : Search blocks in {1}.", DateTime.Now, namepool);
                Mining.Write(String.Format("{0} : Search blocks in {1}.", DateTime.Now, namepool));
                const int n = 2;
                const int countblock = n * 5;
                EFBlockRepository eFBlock = new EFBlockRepository();
                IEnumerable<Block> countbl = eFBlock.Blocks.OrderByDescending(x => x.height).Take(countblock - 1);
                int maxheight = eFBlock.Blocks.OrderByDescending(x => x.height).Select(x => x.height).FirstOrDefault();
                List<int> cntbl = new List<int>();
                int n5 = countbl.Where(x => x.height <= maxheight && x.height >= maxheight - (n - 1) && x.poolname == namepool).Count();
                int n4 = countbl.Where(x => x.height < maxheight - (n - 1) && x.height >= maxheight - (n * 2 - 1) && x.poolname == namepool).Count();
                int n3 = countbl.Where(x => x.height < maxheight - (n * 2 - 1) && x.height >= maxheight - (n * 3 - 1) && x.poolname == namepool).Count();
                int n2 = countbl.Where(x => x.height < maxheight - (n * 3 - 1) && x.height >= maxheight - (n * 4 - 1) && x.poolname == namepool).Count();
                int n1 = countbl.Where(x => x.height < maxheight - (n * 4 - 1) && x.height >= maxheight - (n * 5 - 1) && x.poolname == namepool).Count();

                if (n5 == 0 && n4 == 0 && n3 == 0 && n2 == 0 && n1 == 0)
                {
                    Console.WriteLine("{0} : Not found blocks in {1}.", DateTime.Now, namepool);
                    Mining.Write(String.Format("{0} : Not found blocks in {1}.", DateTime.Now, namepool));
                    Request.ChangePool change = new Request.ChangePool();
                    change.Change(namepool);
                    FoundBlockSuccess = false;
                    TaskChangePool = true;
                    BlockStartWork = maxheight;
                    PoolStartWork = namepool;
                    return;
                }
                else
                {
                    Console.WriteLine("{0} : Found blocks in {1}.", DateTime.Now, namepool);
                    Mining.Write(String.Format("{0} : Found blocks in {1}.", DateTime.Now, namepool));
                    FoundBlockSuccess = true;
                }
            }
            else
            {
                
                EFBlockRepository eFBlock = new EFBlockRepository();
                int maxheight = eFBlock.Blocks.OrderByDescending(x => x.height).Select(x => x.height).FirstOrDefault();
                int WorkFindBlock = eFBlock.Blocks.OrderByDescending(x => x.height).Where(x => x.height <= maxheight && x.height >= BlockStartWork && x.poolname == namepool).Count();
                if (WorkFindBlock >= 6 && namepool=="ViaBTC")
                {
                    TaskChangePool = false;
                    Console.WriteLine("{0} : Work on {1} stopped.", DateTime.Now, namepool);
                    Mining.Write(String.Format("{0} : Work on {1} stopped.", DateTime.Now, namepool));
                    return;
                }
                else if (WorkFindBlock >= 3 && namepool != "ViaBTC")
                {
                    TaskChangePool = false;
                    Console.WriteLine("{0} : Work on {1} stopped.", DateTime.Now, namepool);
                    Mining.Write(String.Format("{0} : Work on {1} stopped.", DateTime.Now, namepool));
                    return;
                }
                    Console.WriteLine("{0} : Work on {1} continue.", DateTime.Now, namepool);
                Mining.Write(String.Format("{0} : Work on {1} continue.", DateTime.Now, namepool));
            }
        }
    }
}
