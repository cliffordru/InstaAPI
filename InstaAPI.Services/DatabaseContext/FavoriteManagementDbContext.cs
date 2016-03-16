using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using InstaAPI.Services.DomainModel;

namespace InstaAPI.Services.DatabaseContext
{
    public class FavoriteManagementDbContext : DbContext
    {
        // Map our 'Favorite' model by convention
        public DbSet<Favorite> Favorites { get; set; }

        public FavoriteManagementDbContext() : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString())
		{ }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Overrides for the convention-based mappings.
            // We're assuming that all our fluent mappings are declared in this assembly.
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(FavoriteManagementDbContext)));
        }
    }
}
