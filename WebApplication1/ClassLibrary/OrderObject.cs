using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DBContext;
using WebApplication1.Models;

namespace WebApplication1.ClassLibrary
{
    /// <summary>
    /// Класс описывающий взаимодействие с заказом
    /// </summary>
    public class OrderObject
    {
        public CartObject Get(ApplicationDBContext context)
        {
            var cartObject = new CartObject { };
           
            
            using (context)
            {
                var statuses = new[] { "Доставлен", "Отменен", "Оплачен" };

                var order = context.Order.Where(i => !(statuses.Contains(i.OrderStatus))).OrderByDescending(i => i.Id).FirstOrDefault();

                if (order != null)
                {
                    context.Product.Load();
                    context.OrderHistory.Load();
                    context.OrderProduct.Load();

                    var orderProducts = context.OrderProduct.Where(i => i.Order == order).ToList();

                    var orderHistory = context.OrderHistory.Where(i => i.Order == order).ToList();

                    cartObject.Id = order.Id;

                    cartObject.Order = order;

                    cartObject.OrderProducts = orderProducts;
                }

                return cartObject;
            }
        } // Get

        public List<OrderForManager> GetForManager(ApplicationDBContext context)
        {
            using (context)
            {
                var orders = context.Order.Where(i => i.OrderStatus != "Отменен").ToList();

                if (orders == null)
                {
                    return null;
                }

                var products = context.Product.Join(
                        context.OrderProduct,
                        p => p.Id,
                        op => op.Product.Id,
                        (p, op) => new OrderProductsForManager
                        {
                            Name = p.Name,
                            Price = p.Price,
                            Amount = op.Amount,
                            ImageLink = p.ImageLink,
                            Order = op.Order
                        }).ToList();

                List<OrderForManager> result = new() { };

                orders.ForEach(i =>
                {
                    result.Add(new OrderForManager
                    {
                        TotalPrice = i.TotalPrice,
                        Products = products.Where(y => y.Order == i).ToList(),
                        OrderHistory = context.OrderHistory.Where(e => e.Order == i).ToList(),
                        OrderDetails = context.OrderDetails.Where(e => e.Order == i).FirstOrDefault()
                    });
                });

                return result;
            }
        } // GetForManager

        public CartObject Update (int productId, string action, ApplicationDBContext context)
        {
            using (context)
            {
                var statuses = new[] { "Доставлен", "Отменен", "Оплачен" };

                var order = context.Order.Where(i => !(statuses.Contains(i.OrderStatus))).FirstOrDefault();

                var product = context.Product.Where(i => i.Id == productId).FirstOrDefault();

                if (order == null)
                {
                    order = new Order
                    {
                        TotalPrice = 0,
                        UserId = 1,
                        OrderStatus = "Формируется"
                    };
                    context.OrderHistory.Add(new OrderHistory
                    {
                        Order = order,
                        OrderStatus = "Формируется",
                        StatusChange = DateTime.Now
                    });
                    context.Order.Add(order);

                    context.SaveChanges();
                }

                var orderProduct = context.OrderProduct.Include(i => i.Order).Where(i => (i.Order.Id == order.Id && i.Product.Id == productId)).FirstOrDefault();

                var orderProducts = context.OrderProduct.Where(i => i.Order == order).ToList();

                var orderHistory = context.OrderHistory.Where(i => i.Order == order).ToList();

                switch (action)
                {
                    case "ADD-PRODUCT":
                        if (orderProduct == null)
                        {
                            context.Add(new OrderProduct
                            {
                                Amount = 1,
                                Product = product,
                                Order = order
                            });
                        } else
                        {
                            orderProduct.Amount++;
                        }
                        AddHistory(context, order, "Изменен");
                        order.TotalPrice += product.Price;
                        break;
                    case "REMOVE-PRODUCT":
                        if (orderProduct.Amount == 1 && orderProducts.Count == 1)
                        {
                            context.Remove(orderProduct);
                            order.OrderStatus = "Отменен";
                            AddHistory(context, order, "Отменен");
                        }
                        if (orderProduct.Amount == 1 && orderProducts.Count > 1)
                        {
                            context.Remove(orderProduct);
                            AddHistory(context, order, "Изменен");
                        }
                        if (orderProduct.Amount > 1)
                        {
                            orderProduct.Amount--;
                            AddHistory(context, order, "Изменен");
                        }
                        order.TotalPrice -= product.Price;
                        break;
                    case "PAID":
                        AddHistory(context, order, "Оплачен");
                        order.OrderStatus = "Оплачен";
                        break;
                    default:
                        break;
                }

                context.SaveChanges();

                return new CartObject
                {
                    Id = order.Id,
                    Order = order,
                    OrderProducts = orderProducts,
                    OrderHistory = orderHistory
                };
            }
        } // Update

        public void Pay(PayOrderReq req, ApplicationDBContext context)
        {
            var order = context.Order.Where(i => i.Id == req.OrderId).FirstOrDefault();
            AddHistory(context, order, "Оплачен");
            order.OrderStatus = "Оплачен";
            context.OrderDetails.Add(new OrderDetails
            {
                Address = req.Address,
                Iban = req.Iban,
                Order = order
            });
            context.SaveChanges();
        } // Pay

        public void Deliver(ApplicationDBContext context, Order order)
        {
            var orderInner = context.Order.Where(i => i == order).FirstOrDefault();
            orderInner.OrderStatus = "Доставлен";
            AddHistory(context, orderInner, "Доставлен");
            context.SaveChanges();
        } // Deliver

        private void AddHistory(ApplicationDBContext context, Order order, string status)
        {
            context.OrderHistory.Add(
                new OrderHistory { 
                    Order = order, 
                    OrderStatus = status, 
                    StatusChange = DateTime.Now 
                });
        } // AddHistory
    } // class
} // namespace 
