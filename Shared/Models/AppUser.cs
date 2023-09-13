using Microsoft.AspNetCore.Identity;

namespace Shared.Models
{
    public class AppUser : IdentityUser
    {
        public string Occupation { get; set; }



    }
}
