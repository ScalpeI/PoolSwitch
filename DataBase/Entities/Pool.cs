using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolSwitch.DataBase.Entities
{
    public class Pool
    {
        [Key]
        public int idpool { get; set; }
        public string owner { get; set; }
        public int priority { get; set; }
        public string type { get; set; }
        public string host { get; set; }
        public int port { get; set; }
        public string user { get; set; }
        public string pass { get; set; }
        public string status { get; set; }
        public string notes { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }
}
