using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Wiser.Data;

namespace Wiser.MVC.Models
{
    //Creating new DbContext by using User class  
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Wisdom> WisdomTable { get; set; }
        public DbSet<Author> AuthorTable { get; set; }
        public DbSet<Favorite> FavoriteTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Conventions
                .Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Wisdom>()
                .HasRequired(w => w.Author);

            modelBuilder.Entity<Favorite>()
                .HasRequired(c => c.User)
                //Ask what this is doing
                //.WithMany(t => t.FavoriteTable)
                //What is this doing?
                //.Map(m => m.MapKey("UserId"))
                ;
        }
    }
    //Creating the primary key for our user
    public class IdentityUserLoginConfiguration : EntityTypeConfiguration<IdentityUserLogin>
    {
        public IdentityUserLoginConfiguration()
        {
            HasKey(iul => iul.UserId);
        }
    }
    //Creating primary key for user role
    public class IdentityUserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
    {
        public IdentityUserRoleConfiguration()
        {
            HasKey(iur => iur.UserId);
        }
    }
}