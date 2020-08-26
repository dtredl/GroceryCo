namespace GroceryCo.Models
{
    /// <summary>
    /// A MinimumQuantityPromotion is a promotion where purchasing enough units of an item to meet some threshold will allow them all to be purchased at
    /// a set discounted unit price.
    /// </summary>
    class MinimumQuantityPromotion : Promotion
    {
        public int MinimumQuantity { get; set; }
        public double SaleUnitPrice { get; set; }
        public override string DiscountText
        {
            get
            {
                return $">{this.MinimumQuantity} @{this.SaleUnitPrice}";
            }
        }

        public override bool DoesQuantityQualify(int quantity)
        {
            return quantity >= this.MinimumQuantity;
        }

        public override double ApplyDiscount(int quantity, double basePrice)
        {
            return quantity * this.SaleUnitPrice;
        }
    }
}
