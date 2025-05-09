using Microsoft.AspNetCore.Identity;

namespace webapphotel.Model
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
    }
}
