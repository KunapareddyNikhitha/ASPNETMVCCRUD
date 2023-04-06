using System.ComponentModel.DataAnnotations;

namespace ASPNETMVCCRUD.Models.Domain
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public bool ProductAvailability { get; set; }

    }
}
