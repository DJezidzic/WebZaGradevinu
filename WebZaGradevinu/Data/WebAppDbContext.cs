using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebZaGradevinu.Data
{
    public class WebAppDbContext : DbContext
    {

        protected WebAppDbContext() { }

        public WebAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ImageLists> ImageListing { get; set; }

    }
}
