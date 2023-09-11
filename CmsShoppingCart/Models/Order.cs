
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsShoppingCart.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "User")]
        public string UserName { get; set; }

        [ForeignKey("Product")]
        public string ProductName { get; set; } //product name
        public string PaymentMethod { get; set; }



    }
}
