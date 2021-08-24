using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebConsultarProductos.Models
{
    public class ProductResponse
    {
        public string id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public AccountGroup account_group { get; set; }
        public string type { get; set; }
        public bool stock_control { get; set; }
        public bool active { get; set; }
        public string tax_classification { get; set; }
        public bool tax_included { get; set; }
        public double tax_consumption_value { get; set; }
        public List<Tax> taxes { get; set; }
        public List<Price> prices { get; set; }
        public string reference { get; set; }
        public string description { get; set; }
        public AdditionalFields additional_fields { get; set; }
        public double available_quantity { get; set; }
        public List<Warehouse> warehouses { get; set; }
        public Metadata metadata { get; set; }
    }
}
