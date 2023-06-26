using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebASP.Data.Entities
{
    public class OrderDetail
    {

        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { set; get; }

        public Order Order { get; set; }

        public Product Product { get; set; }

    }
}
