namespace ASPNETMVCCRUD.Models
{
    public class UpdateProductViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public bool ProductAvailability { get; set; }
    }
}
