using PoolSwitch.DataBase.Abstract;
using PoolSwitch.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolSwitch.DataBase.Concrete
{
    /// <summary>
    /// Логика взаимодействия с сущностью User
    /// </summary>
    class EFUserRepository : IUserRepository
    {
        EFDBContext context = new EFDBContext();
        public IEnumerable<User> Users
        {
            get { return context.Users; }
            set { }
        }
    }
}
