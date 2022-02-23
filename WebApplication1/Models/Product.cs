using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Title { get; set; }

        public int Price { get; set; }

        /// <summary>
        /// Предположим что у нас развернут CDN для хостинга изображений товара
        /// </summary>
        public string ImageLink { get; set; }
    }
}
