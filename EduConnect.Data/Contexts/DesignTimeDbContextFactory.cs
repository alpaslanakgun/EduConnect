using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using EduConnect.Data.Context;
using EduConnect.Data.Configurations;

namespace EduConnect.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer(Configuration.ConnectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
