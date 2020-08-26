namespace GroceryCo.Models
{
    /// <summary>
    /// A BasicPromotion is a promotion where every unit is purchased at a set discounted price.
    /// </summary>
    class BasicPromotion : Promotion
    {
        public double SaleUnitPrice { get; set; }
        public override string DiscountText
        {
            get
            {
                return $"@{this.SaleUnitPrice}";
            }
        }

        public override bool DoesQuantityQualify(int quantity)
        {
            return true;
        }

        public override double ApplyDiscount(int quantity, double basePrice)
        {
            return quantity * this.SaleUnitPrice;
        }
    }
}
