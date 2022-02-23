using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DBContext;
using WebApplication1.ClassLibrary;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OnlineShopController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public OnlineShopController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("products")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = new ProductObject(_context);
            return Ok(await products.GetProduct());
        }

        [HttpGet]
        [Route("cart")]
        public ActionResult<CartObject> GetCart()
        {
            var cart = new CartObject();
            return cart.GetCart(_context);
        }

        [HttpGet]
        [Route("cart/manager")]
        public ActionResult<List<OrderForManager>> GetForManager()
        {
            var cart = new CartObject();
            return cart.GetCartForManager(_context);
        }

        [HttpPost]
        [Route("cart/manager/deliver")]
        public ActionResult DeliverOrder(Order order)
        {
            var cart = new CartObject();
            cart.DeliverOrder(_context, order);
            return Ok();
        }

        [HttpPost]
        [Route("cart/update")]
        public ActionResult<CartObject> UpdateCart(UpdateCartReq req)
        {
            var cart = new CartObject();
            return cart.UpdateCart(req, _context);
        }

        [HttpPost]
        [Route("order/pay")]
        public ActionResult<CartObject> PayOrder(PayOrderReq req)
        {
            var cart = new CartObject();
            cart.PayOrder(req, _context);
            return Ok();
        }
    }
}
