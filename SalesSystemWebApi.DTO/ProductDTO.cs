using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystemWebApi.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        public string? Name { get; set; }

        public int? Stock { get; set; }

        public string? Price { get; set; }

        public int? IsActive { get; set; }

        public int? CategoryId { get; set; }

        public string? CategoryDescription { get; set; }

    }
}
