using System;

namespace GroceryCo.Models
{
    /// <summary>
    /// This abstract class defines a promotion.
    /// All promotions must have a start and end date. If a price doesn't, then it isn't really a promotion, just a new base price.
    /// Promotions are required to provide DiscountText for displaying on receipts as well as answering whether a quantity of an item qualifies for the specific promotion
    /// and what the total sale price would be if this promotion is applied.
    /// Since the promotion knows its own start and end date, it is capable of calculating whether the current date falls between them.
    /// </summary>
    abstract class Promotion
    {
        public DateTime StartDate;
        public DateTime EndDate;

        public virtual string DiscountText
        {
            get
            {
                throw new NotImplementedException($"DiscountText not implemented at {this.GetType()}");
            }
        }

        public virtual bool DoesQuantityQualify(int quantity)
        {
            throw new NotImplementedException($"DoesQuantityQualify not implemented at {this.GetType()}");
        }

        public virtual double ApplyDiscount(int quantity, double basePrice)
        {
            throw new NotImplementedException($"ApplyDiscount not implemented at {this.GetType()}");
        }

        public bool IsCurrent()
        {
            return this.StartDate < DateTime.Now && this.EndDate > DateTime.Now;
        }
    }
}
