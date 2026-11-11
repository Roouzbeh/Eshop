using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogService.Domain.Product
{
    public class Product
    {
        private Product() { }

        public Guid Id { get; private set; }
        public string Name { get; private set; } = "";
        public int Stock { get; private set; }

        public static Product Create(string name, int stock) =>
            new() { Id = Guid.NewGuid(), Name = name, Stock = stock };

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            if (Stock < quantity) throw new InvalidOperationException("موجودی کافی نیست");
            Stock -= quantity;
        }

        public void ResetStock(int stock) => Stock = stock;
    }
}
