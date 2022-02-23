using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ClassLibrary
{
    public class UpdateCartReq
    {
        public string Action { get; set; }

        public int ProductId { get; set; }
    }
}
