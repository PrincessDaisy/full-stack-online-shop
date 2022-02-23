using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }

        public string Address { get; set; }

        public string Iban { get; set; }

        public Order Order { get; set; }
    }
}
