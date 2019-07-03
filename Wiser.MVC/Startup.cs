using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Wiser.Data;
using Wiser.Models;

[assembly: OwinStartupAttribute(typeof(Wiser.MVC.Startup))]
namespace Wiser.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateAdmin();
        }
        private void CreateAdmin()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ctx));
            UserManager<User> userManager = new UserManager<User>(new UserStore<User>(ctx));

            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }
            if (!roleManager.RoleExists("User"))
            {
                roleManager.Create(new IdentityRole("User"));

            }
            if (userManager.FindByEmail("admin@admin.com") == null)
            {

                //Creating new "Super user"
                var user = new User();
                user.Email = "admin@admin.com";
                user.UserName = "admin@admin.com";
                user.FirstName = "Nicholas";
                user.LastName = "Perry";
                string userPWD = "Password13@";
                var chkUser = userManager.Create(user, userPWD);

                //Add default User to Role Admin  
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }

}
