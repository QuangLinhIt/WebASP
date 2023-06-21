using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebASP.Data.EF
{
    public class WebASPDbContextFactory : IDesignTimeDbContextFactory<WebASPDbContext>
    {
        public WebASPDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
          
            var connectionString = configuration.GetConnectionString("WebASPDb");
           
            var optionsBuilder = new DbContextOptionsBuilder<WebASPDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=WebASP;Trusted_Connection=True;");

            return new WebASPDbContext(optionsBuilder.Options);
        }
    }
}
