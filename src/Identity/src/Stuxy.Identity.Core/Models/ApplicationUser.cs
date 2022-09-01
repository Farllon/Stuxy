using Microsoft.AspNetCore.Identity;

namespace Stuxy.Identity.Core.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; }

        public string? ImageProfileUrl { get; set; }
    }
}
