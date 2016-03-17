using System.Configuration;
using System.Data.Entity;
using System.Reflection;
using InstaAPI.Services.DomainModel;

namespace InstaAPI.Services.DatabaseContext
{
    public class FavoriteManagementDbContext : DbContext
    {
        // Map our 'Favorite' model by convention
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Post> Posts { get; set; }

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
