using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buibaquang_aspcoreblazor.Models.Models
{
    public class ProductListSearch
    {
        public string? Name { get; set; }
        public Guid? CategoryId { get; set; }
        public double? Price { get; set; }
    }
}
