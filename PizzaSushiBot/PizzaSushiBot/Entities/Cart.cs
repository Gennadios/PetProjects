using PizzaSushiBot.Models;
using System.Collections.Generic;

namespace PizzaSushiBot.Entities
{
    sealed class Cart
    {
        private decimal _grandTotal;
        private int _numberOfItems;

        internal Dictionary<Product, int> Purchases { get; set; } = new();
        internal Stack<Product> ProductStack { get; set; } = new();
        internal decimal GrandTotal
        {
            get
            {
                _grandTotal = 0;

                foreach (var entry in Purchases)
                    _grandTotal += entry.Key.Price * entry.Value;

                return _grandTotal;
            }
        }
        internal int NumberOfItems
        {
            get
            {
                _numberOfItems = 0;

                foreach (var entry in Purchases)
                    _numberOfItems += entry.Value;

                return _numberOfItems;
            }
        }

        internal void AddToCart(Product product)
        {
            if (!Purchases.ContainsKey(product))
                Purchases.Add(product, 1);
            else
                Purchases[product]++;

            ProductStack.Push(product);
        }

        internal void PopFromCart()
        {
            bool poppable = ProductStack.TryPeek(out Product p);

            if (poppable && Purchases.ContainsKey(p))
            {
                Purchases[p]--;
                ProductStack.Pop();

                if (Purchases[p] == 0)
                    Purchases.Remove(p);
            }
            else
                return;
        }

        internal string GetPurchasesList()
        {
            string resultString = string.Empty;

            foreach (var p in Purchases)
                resultString += $"{p.Key.Name} x {p.Value}\n";

            resultString += $"Grand total: {GrandTotal} RUB";

            return resultString;
        }
    }
}
