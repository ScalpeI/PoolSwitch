using PoolSwitch.DataBase.Abstract;
using PoolSwitch.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolSwitch.DataBase.Concrete
{
    public class EFParseRepository : IParseRepository
    {
        EFDBContext context = new EFDBContext();
        public IEnumerable<Parse> Parses
        {
            get { return context.Parses; }
            set { }
        }
        public async Task Create(string poolname, string hash)
        {
                context.Parses.Add(new Parse { poolname = poolname, hash = hash,date=DateTime.UtcNow });
                context.SaveChangesAsync().Wait();
        }
    }
}
