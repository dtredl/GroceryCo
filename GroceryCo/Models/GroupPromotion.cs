namespace GroceryCo.Models
{
    /// <summary>
    /// A GroupPromotion is a promotion where buying a number of units that make up a predefined group size will offer a discounted price for the group.
    /// Any additional units are purchased at the base unit price.
    /// </summary>
    class GroupPromotion : Promotion
    {
        public int GroupSize { get; set; }
        public double GroupPrice { get; set; }
        public override string DiscountText
        {
            get
            {
                return $"@{this.GroupPrice}/{this.GroupSize}";
            }
        }

        public override bool DoesQuantityQualify(int quantity)
        {
            return quantity >= this.GroupSize;
        }

        public override double ApplyDiscount(int quantity, double basePrice)
        {
            var groupings = quantity / this.GroupSize;
            return ((quantity - (groupings * this.GroupSize)) * basePrice) + (groupings * this.GroupPrice);
        }
    }
}
