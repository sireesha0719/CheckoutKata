using System;
using System.Collections.Generic;

namespace CheckoutKata
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up the items and prices
            var itemPrices = new Dictionary<char, decimal>()
            {
                {'A', 10m},
                {'B', 15m},
                {'C', 40m},
                {'D', 55m}
            };

            // Create a new checkout
            var checkout = new Checkout(itemPrices);

            // Add items to the checkout
            checkout.CheckoutItems('A');
            checkout.CheckoutItems('B');
            checkout.CheckoutItems('C');
            checkout.CheckoutItems('D');
            checkout.CheckoutItems('A');
            checkout.CheckoutItems('B');
            checkout.CheckoutItems('A');
            checkout.CheckoutItems('A');

            // Calculate the total cost
            var total = checkout.Total();

            Console.WriteLine($"Total: {total:C}");
        }
    }

    public class Checkout
    {
        private readonly Dictionary<char, decimal> _itemPrices;
        private readonly Dictionary<char, int> _items;
        private readonly Dictionary<char, Tuple<int, decimal>> _promotions;

        public Checkout(Dictionary<char, decimal> itemPrices)
        {
            _itemPrices = itemPrices;
            _items = new Dictionary<char, int>();
            _promotions = new Dictionary<char, Tuple<int, decimal>>();
        }

        public void CheckoutItems(char item)
        {
            // Add the item to the basket
            if (_items.ContainsKey(item))
            {
                _items[item]++;
            }
            else
            {
                _items[item] = 1;
            }

            // Check for any promotions
            if (item == 'B' && _items[item] % 3 == 0)
            {
                _promotions[item] = new Tuple<int, decimal>(_items[item] / 3, 40m);
            }
            else if (item == 'D' && _items[item] % 2 == 0)
            {
                _promotions[item] = new Tuple<int, decimal>(_items[item] / 2, 0.25m);
            }
        }

        public decimal Total()
        {
            var total = 0m;

            // Calculate the total cost of each item
            foreach (var item in _items)
            {
                var price = _itemPrices[item.Key];

                // Apply any promotions
                if (_promotions.ContainsKey(item.Key))
                {
                    var promo = _promotions[item.Key];
                    var promoCount = promo.Item1;
                    var promoPrice = promo.Item2;
                    var regularCount = item.Value - (promoCount * 3);

                    price = (promoCount * promoPrice) + (regularCount * _itemPrices[item.Key]);
                }
                else
                {
                    price = item.Value * _itemPrices[item.Key];
                }

                total += price;
            }

            return total;
        }

        public Dictionary<char, int> Items
        {
            get
            {
                return _items;
            }
        }
    }
}
