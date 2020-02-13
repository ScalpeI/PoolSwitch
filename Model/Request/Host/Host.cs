using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolSwitch.Model.Request.Host
{
    public class Host
    {
        public string poolname { get; set; }
        public string host { get; set; }
        public string port { get; set; }
        public string user { get; set; }
        public string pass { get; set; }
        public string priority { get; set; }
    }
}
