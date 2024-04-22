using Microsoft.AspNetCore.Identity;

namespace DemoRegAndLoginWithIdentity.Models
{
    public class ApplicationUser: IdentityUser
    {
        public String Firstname { get; set; }

        public string Lastname { get; set; }    
    }
}
