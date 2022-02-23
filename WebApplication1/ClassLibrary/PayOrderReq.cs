using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ClassLibrary
{
    public class PayOrderReq
    {
        public int OrderId { get; set; }

        public string Address { get; set; }

        public string Iban { get; set; }
    }
}
