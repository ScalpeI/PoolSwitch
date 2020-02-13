using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolSwitch.DataBase.Entities
{
    public class Block
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int height { get; set; }
        public string poolname { get; set; }
        public int timestamp { get; set; }
    }
}
