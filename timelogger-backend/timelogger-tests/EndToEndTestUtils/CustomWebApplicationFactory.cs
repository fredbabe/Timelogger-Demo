using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using timelogger_web_api.Data;
using timelogger_web_api.Models.Entities;

namespace timelogger_tests.EndToEndTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public Guid SeededCustomerId { get; set; }
        public Guid SeededProjectId { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Force the environment to 'Test'
            builder.UseEnvironment("Test");

            builder.ConfigureServices(async services =>
            {
                // Remove *all* existing registrations for AppDbContext
                var descriptors = services
                    .Where(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>))
                    .ToList();

                foreach (var descriptor in descriptors)
                {
                    services.Remove(descriptor);
                }

                // Now register InMemory
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase($"TestDB");
                });

                // 3. Build the service provider to create a scope
                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AppDbContext>();

                    // 4. Ensure a clean database
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();

                    // 5. Seed data
                    SeededCustomerId = Guid.NewGuid();
                    SeededProjectId = Guid.NewGuid();

                    db.Customers.Add(new Customer
                    {
                        Id = SeededCustomerId,
                        Name = "Test Customer"
                    });

                    db.Projects.Add(new Project
                    {
                        Id = SeededProjectId,
                        Name = "Test Project",
                        CustomerId = SeededCustomerId
                    });

                    // Save your changes
                    await db.SaveChangesAsync();


                }
            });
        }
    }

}
