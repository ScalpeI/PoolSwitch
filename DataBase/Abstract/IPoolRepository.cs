using PoolSwitch.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolSwitch.DataBase.Abstract
{
    public interface IPoolRepository
    {
        IEnumerable<Pool> Pools { get; set; }

    }
}
