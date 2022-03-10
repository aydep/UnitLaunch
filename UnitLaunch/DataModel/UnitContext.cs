using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UnitLaunch.DataModel
{
    public class UnitContext : DbContext
    {
        public DbSet<Game> Games => Set<Game>();
        public UnitContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=UnitLaunchDB;Trusted_Connection=True;");
        }
    }
}
