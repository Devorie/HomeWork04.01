using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HomeWork04._01.Data
{
    public class QuestionContextFactory : IDesignTimeDbContextFactory<QuestionContext>
    {
        public QuestionContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}HomeWork04.01.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new QuestionContext(config.GetConnectionString("ConStr"));
        }
    }
}
