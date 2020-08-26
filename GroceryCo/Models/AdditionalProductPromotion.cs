namespace GroceryCo.Models
{
    /// <summary>
    /// An AdditionalProductPromotion is a promotion where buying one unit of an item at full price causes the next unit of the same item to be purchased at a discount.
    /// AdditionalProductDiscount of 1 indicates a 100% discount or, in other words, a free second item.
    /// "BOGO" is used as short-hand for "Buy One, Get One"
    /// </summary>
    class AdditionalProductPromotion : Promotion
    {
        public double AdditionalProductDiscount { get; set; }
        public override string DiscountText
        {
            get
            {
                if (this.AdditionalProductDiscount == 1)
                {
                    return "BOGO";
                }
                else
                {
                    return $"BOGO {this.AdditionalProductDiscount:P0} off";
                }
            }
        }

        public override bool DoesQuantityQualify(int quantity)
        {
            return quantity > 1;
        }

        public override double ApplyDiscount(int quantity, double basePrice)
        {
            var pairs = quantity / 2;
            var extra = quantity - (pairs * 2);
            return ((pairs + extra) * basePrice) + ((pairs * basePrice) * (1 - this.AdditionalProductDiscount));
        }
    }
}
