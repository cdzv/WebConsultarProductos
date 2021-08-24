using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebConsultarProductos.Models
{
    public class Price
    {
        public string currency_code { get; set; }
        public List<PriceList> price_list { get; set; }
    }
}
