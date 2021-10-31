using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebZaGradevinu.Data
{
    public class WebAppDbContext : IdentityDbContext<AppUser>
    {
        protected WebAppDbContext() { }
        public WebAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ImageLists> ImageListing { get; set; }
        public DbSet<Jobs> JobsListing { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<AcceptedJobs> AcceptedJobsList { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }


    }
}
