using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class EarlyBirdsContext : DbContext
    {
        public EarlyBirdsContext() : base("EarlyBirds") { }

        public DbSet<Issue> Issues { get; set; }
        public DbSet<Website> Websites { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Agent> Agents { get; set; }
    }
}
