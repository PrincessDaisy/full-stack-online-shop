using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ClassLibrary
{
    public class OrderProductsForManager
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public int Amount { get; set; }

        public string ImageLink { get; set; }

        public Order Order { get; set; }
    }
}
