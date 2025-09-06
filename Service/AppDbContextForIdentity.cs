using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace JWTbackendWithWebAPI.Service
{
    public class AppDbContextForIdentity : IdentityDbContext<IdentityUser>
    {
        public AppDbContextForIdentity(DbContextOptions<AppDbContextForIdentity> options) : base(options)
        {
        }

        //public AppDbContextForIdentity()
        //{
        //}
    }
}
