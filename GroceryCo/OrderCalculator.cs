using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using GroceryCo.Models;
using GroceryCo.Interfaces;

namespace GroceryCo
{
    /// <summary>
    /// This OrderCalculator is the main workhorse of this program.
    /// It uses an inventory repository and a specific order to calculate the total and output a receipt to the screen.
    /// </summary>
    class OrderCalculator
    {
        IInventoryRepository inventoryRepository;

        public OrderCalculator(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        public string CalculateOrderOutput(Dictionary<string, int> order)
        {
            var stringBuilder = new StringBuilder();
            var inventory = inventoryRepository.GetInventoryItems();
            var billTotal = 0.0d;
            var savingsTotal = 0.0d;

            foreach (var key in order.Keys)
            {
                var inventoryItem = inventory.FirstOrDefault(i => i.Name.ToLower() == key);
                if (inventoryItem == null)
                {
                    throw new InvalidDataException($"Item not found in inventory: {key}");
                }

                var itemTotal = Math.Round(order[key] * inventoryItem.UnitPrice, 2);
                stringBuilder.AppendLine($"{key} x{order[key]} @{inventoryItem.UnitPrice} = {itemTotal:C}");

                var bestPromotion = inventoryItem.FindBestPromotion(order[key]);

                if (bestPromotion != null)
                {
                    var saleTotal = bestPromotion.ApplyDiscount(order[key], inventoryItem.UnitPrice);
                    stringBuilder.AppendLine($" ON SALE {bestPromotion.DiscountText} = {saleTotal:C}");

                    savingsTotal += itemTotal - saleTotal;
                    itemTotal = saleTotal;
                }

                billTotal += itemTotal;
            }

            stringBuilder.AppendLine($"\nTotal = {billTotal:C}");
            if (savingsTotal > 0)
            {
                stringBuilder.AppendLine($"You saved {savingsTotal:C} today!");
            }

            return stringBuilder.ToString();
        }
    }
}
