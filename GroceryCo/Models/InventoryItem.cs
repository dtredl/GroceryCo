using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryCo.Models
{
    /// <summary>
    /// InventoryItem defines a single item in the store's inventory complete with a name, a base unit price, and a collection of promotions.
    /// Since this class knows its own price and all promotions that can possibly be applied, it is capable of determining which of those promotions offers the best deal.
    /// </summary>
    class InventoryItem
    {
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public IEnumerable<Promotion> Promotions { get; set; }

        public Promotion FindBestPromotion(int quantity)
        {
            var currentPromotions = this.Promotions.Where(p => p.IsCurrent());
            if (currentPromotions.Count() < 1)
            {
                return null;
            }

            var bestPrice = quantity * this.UnitPrice;
            Promotion bestPromotion = null;

            foreach (Promotion promotion in currentPromotions)
            {
                var promotionPrice = bestPrice;
                if (promotion != null && promotion.DoesQuantityQualify(quantity))
                {
                    promotionPrice = promotion.ApplyDiscount(quantity, this.UnitPrice);
                }

                if (promotionPrice < bestPrice)
                {
                    bestPrice = promotionPrice;
                    bestPromotion = promotion;
                }
            }

            return bestPromotion;
        }
    }
}
