using PoolSwitch.DataBase.Abstract;
using PoolSwitch.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolSwitch.DataBase.Concrete
{
    public class EFBlockRepository : IBlockRepository
    {
        EFDBContext context = new EFDBContext();
        public IEnumerable<Block> Blocks
        {
            get { return context.Blocks; }
            set { }
        }
        public async Task Create(int height, int timestamp, string poolname)
        {
            //if (context.Blocks.Where(x => x.height == height).Count() == 0)
            //{
                context.Blocks.Add(new Block { height = height, poolname = poolname, timestamp = timestamp });
                context.SaveChangesAsync().Wait();
            //}
        }
    }
}
