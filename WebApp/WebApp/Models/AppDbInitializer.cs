using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using DAL;
using DAL.Entities;

// Need add to Global.asax to AppStart method after DB Migration

namespace WebApp.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //Creating roles
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "recruiter" };
            var role3 = new IdentityRole { Name = "seeker" };

            //Adding roles to db
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            base.Seed(context);
        }
    }
}