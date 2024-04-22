using DemoRegAndLoginWithIdentity.Models;
using DemoRegAndLoginWithIdentity.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoRegAndLoginWithIdentity.Data
{
    public class MyDbContext : IdentityDbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
            

        }

        public DbSet<Student> Students { get; set; }

        //Vaishanvi
        public DbSet<Class> classes { get; set; }
        public object Class { get; internal set; }

        //crud
        public DbSet<Patient> Patients { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

    }
}
