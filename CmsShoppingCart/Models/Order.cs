
using System.ComponentModel.DataAnnotations.Schema;

namespace CmsShoppingCart.Models
{
    public class Order
    {
        public int Id { get; set; }
        
       // [ForeignKey("UserName")]
       // public User User { get; set; }
        public string PaymentMethod { get; set; }
    }
}
