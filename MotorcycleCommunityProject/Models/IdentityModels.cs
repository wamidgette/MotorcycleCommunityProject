using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MotorcycleCommunityProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class MotorcycleCommunityContext : IdentityDbContext<ApplicationUser>
    {
        public MotorcycleCommunityContext()
            : base("name=MotorcycleDbContext", throwIfV1Schema: false)
        {
        }

        public static MotorcycleCommunityContext Create()
        {
            return new MotorcycleCommunityContext();
        }

        //Create database from models 
        public DbSet<Rider> Riders { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}