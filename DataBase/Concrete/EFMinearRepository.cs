using PoolSwitch.DataBase.Abstract;
using PoolSwitch.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolSwitch.DataBase.Concrete
{
    public class EFMinearRepository : IMinearRepository
    {
        EFDBContext context = new EFDBContext();
        public IEnumerable<Minear> Minears
        {
            get { return context.Minears; }
            set { }
        }
        //public string btcMinear { get { return context.Minears.OrderBy(x => x.date).Select(x => x.fpps_mining_earnings).LastOrDefault(); } }
    }
}
