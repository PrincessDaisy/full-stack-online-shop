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
    /// Класс описывающий взаимодействие с товарами (Пока только один метод, в будущем возможно добавятся еще)
    /// </summary>
    public class ProductObject : Product
    {
        public int? Amount { get; set; }

        private readonly ApplicationDBContext _context;

        public ProductObject()
        {

        }

        public ProductObject(ApplicationDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Метод возвращающий список всех товаров
        /// </summary>
        /// <returns>Список товаров</returns>
        public async Task<List<ProductObject>> GetProduct()
        {
            using (_context)
            {
                var statuses = new[] { "Доставлен", "Отменен", "Оплачен" };

                var order = _context.Order.Where(i => !(statuses.Contains(i.OrderStatus))).OrderByDescending(i => i.Id).FirstOrDefault();

                var res = await _context.Product.GroupJoin(
                        _context.OrderProduct.Where(i => !(statuses.Contains(i.Order.OrderStatus))),
                        i => i.Id,
                        e => e.Product.Id,
                        (i, e) => new
                        {
                            i,
                            e,
                        }
                    ).SelectMany(
                        x => x.e.DefaultIfEmpty(),
                        (a,b) => new ProductObject
                        {
                            Id = a.i.Id,
                            Name = a.i.Name,
                            Title = a.i.Title,
                            Price = a.i.Price,
                            ImageLink = a.i.ImageLink,
                            Amount = b.Amount != null ? (order == null ? 0: b.Amount) : 0
                        }
                    ).ToListAsync();

                return res;
            }
        } // GetProduct
    } // class
} // namespace
