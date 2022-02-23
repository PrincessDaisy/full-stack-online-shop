using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class OrderHistory
    {
        [Key]
        public int Id { get; set; }

        public DateTime StatusChange { get; set; }

        public string OrderStatus { get; set; }

        public Order Order { get; set; }
    }
}
