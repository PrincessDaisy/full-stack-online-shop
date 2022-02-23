using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ClassLibrary
{
    public class OrderForManager
    {
        public int TotalPrice { get; set; }

        public List<OrderProductsForManager> Products { get; set; }

        public List<OrderHistory> OrderHistory { get; set; }

        public OrderDetails OrderDetails { get; set; }
    }
}
