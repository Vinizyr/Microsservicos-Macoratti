namespace Basket.API.Entities
{
    public class BasketCheckout
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAdress { get; set; }
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public int Expiration { get; set; }
        public string CVV { get; set; }
        public int PaymentMethod { get; set; }
        // Optional: Override ToString for easier debugging
        public override string ToString()
        {
            return $"{FirstName} {LastName} - Total: ${TotalPrice}";
        }
    }
}
