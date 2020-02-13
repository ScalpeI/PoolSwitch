using PoolSwitch.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolSwitch.DataBase.Abstract
{
    /// <summary>
    /// Интерфейс для сущности User
    /// </summary>
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; set; }
    }
}
