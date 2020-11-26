using Microsoft.AspNetCore.Identity;

namespace Repository.Models
{
    public class EFUser : IdentityUser<int>
    {
        public string Name { get; set; }
        
        public string Surname { get; set; }

        public string Userpic { get; set; }
    }
}