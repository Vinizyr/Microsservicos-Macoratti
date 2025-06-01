namespace Basket.API.Entities
{
    public class ShoppingCartItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        // Optional: Override ToString for easier debugging
        public override string ToString()
        {
            return $"{ProductName} (x{Quantity}) - ${Price * Quantity}";
        }
    }
}