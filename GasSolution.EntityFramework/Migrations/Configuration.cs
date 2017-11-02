using System.Data.Entity.Migrations;

namespace GasSolution.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<GasSolution.EntityFramework.GasSolutionDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "GasSolution";
        }

        protected override void Seed(GasSolution.EntityFramework.GasSolutionDbContext context)
        {
            // This method will be called every time after migrating to the latest version.
            // You can add any seed data here...
        }
    }
}
