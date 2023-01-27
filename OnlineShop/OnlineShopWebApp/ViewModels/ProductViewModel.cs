using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.VeiwModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Не указано название продукта")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Имя должно содержать от 2 до 25 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана цена продукта")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Не указано описание продукта")]
        public string Description { get; set; }
        public List<string> ImagesPaths { get; set; }
        public string ImagePath => ImagesPaths.Count == 0 ? "/images/Products/image1.jpg" : ImagesPaths[0];
        public bool IsFavorite { get; set; }

    }
}
