using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DBContext;
using WebApplication1.Models;

namespace WebApplication1.ClassLibrary
{
    /// <summary>
    /// Класс для взаимодействия с корзиной
    /// </summary>
    public class CartObject : Cart
    {
        public List<OrderProduct> OrderProducts { get; set; }

        public List<OrderHistory> OrderHistory { get; set; }

        public CartObject GetCart(ApplicationDBContext context)
        {
            var orderObject = new OrderObject();
            return orderObject.Get(context);
        } // GetCart

        public List<OrderForManager> GetCartForManager(ApplicationDBContext context)
        {
            var orderObject = new OrderObject();
            return orderObject.GetForManager(context);
        } // GetCartForManager

        public void DeliverOrder(ApplicationDBContext context, Order order)
        {
            var orderObject = new OrderObject();
            orderObject.Deliver(context, order);
        } // DeliverOrder

        public CartObject UpdateCart(UpdateCartReq req, ApplicationDBContext context)
        {
            var orderObject = new OrderObject();
            return orderObject.Update(req.ProductId, req.Action, context);

        } // UpdateCart

        public void PayOrder(PayOrderReq req, ApplicationDBContext context)
        {
            var orderObject = new OrderObject();
            orderObject.Pay(req, context);

        } // UpdateCart

    } // class
} // namespace
