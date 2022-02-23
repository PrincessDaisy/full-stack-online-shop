using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }
        
        public Product Product { get; set; }

        public Order Order { get; set; }
    }
}
