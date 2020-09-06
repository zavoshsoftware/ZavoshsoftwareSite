using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Models
{
   public class DatabaseContext:DbContext
    {
        static DatabaseContext()
        {
             System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageGroup> PageGroups { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PagePosition> PagePositions { get; set; }
        public DbSet<ContactUsForm> ContactUsForms { get; set; }
        public DbSet<PortfolioGroup> PortfolioGroups { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<Faq> Faqs { get; set; }
 

    }
}
