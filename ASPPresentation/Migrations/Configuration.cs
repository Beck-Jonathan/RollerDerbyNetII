namespace ASPPresentation.Migrations
{
    using ASPPresentation.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ASPPresentation.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ASPPresentation.Models.ApplicationDbContext";
        }

        protected override void Seed(ASPPresentation.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            const string admin = "admin@company.com";
            const string adminPassword = "P@ssw0rd";
            //step 6
            LogicLayer.SkaterManager skaterManager = new LogicLayer.SkaterManager();
            var roles = skaterManager.RetreiveSkaterroles();
            foreach (var role in roles)
            {
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = role });
            }
            if (!roles.Contains("Administrator"))
            {
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Administrator" });
            }
            var userStore = new UserStore<ApplicationUser>(context);
            var usermanager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(u => u.UserName == admin))
            {
                var user = new ApplicationUser()
                {
                    UserName = admin,
                    Email = admin,
                    GivenName = "Admin",
                    FamilyName = "Company"


                };

                IdentityResult result = usermanager.Create(user, adminPassword);
                context.SaveChanges(); //udpates the database
                if (result.Succeeded)
                {
                    usermanager.AddToRole(user.Id, "Administrator");
                    context.SaveChanges();
                }

            }

        }
    }
}
