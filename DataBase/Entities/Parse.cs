using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolSwitch.DataBase.Entities
{
    public class Parse
    {
        [Key]
        public int id { get; set; }
        public string poolname { get; set; }
        public string hash { get; set; }
        public DateTime date { get; set; }
    }
}
