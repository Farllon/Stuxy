using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Stuxy.Identity.Core.Models;

namespace Stuxy.Identity.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<
        ApplicationUser, 
        ApplicationRole, 
        Guid, 
        ApplicationUserClaim, 
        ApplicationUserRole, 
        ApplicationUserLogin, 
        ApplicationRoleClaim, 
        ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}
