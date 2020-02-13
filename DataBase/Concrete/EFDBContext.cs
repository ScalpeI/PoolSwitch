using PoolSwitch.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolSwitch.DataBase.Concrete
{
    class EFDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Pool> Pools { get; set; }
        public DbSet<Parse> Parses { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Minear> Minears { get; set; }
        public EFDBContext() : base("EFDBContext")
        {

        }
    }
}
