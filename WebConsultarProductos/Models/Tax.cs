using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebConsultarProductos.Models
{
    public class Tax
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public double percentage { get; set; }
    }
}
