using PoolSwitch.DataBase.Abstract;
using PoolSwitch.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolSwitch.DataBase.Concrete
{
    public class EFPoolRepository : IPoolRepository
    {
        EFDBContext context = new EFDBContext();
        public IEnumerable<Pool> Pools
        {
            get { return context.Pools; }
            set { }
        }
    }
}
