namespace Checkout.Models
{
    public class Product
    {
        public bool IsMultibuy { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceDiscounted { get; set; }
        public int? MultibuyQuantity { get; set; }
    }
}
