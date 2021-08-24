using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebConsultarProductos.Models
{
    public class ApiResponse
    {
        public Pagination pagination { get; set; }
        public List<ProductResponse> results { get; set; }
        public Link _links { get; set; }
    }
}
