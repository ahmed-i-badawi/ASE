﻿using Shipping.Application.Common.Interfaces;
using Shipping.Infrastructure.Identity;
using Shipping.Infrastructure.Persistence;
using Shipping.Shared.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shipping.WebUI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();


            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    //Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");

                    if (context.Database.IsNpgsql())
                    {
                        //context.Database.EnsureDeleted();
                        string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                        if (env == "Testing")
                        {
                            context.Database.EnsureCreated();
                        }
                        //dContext.Database.EnsureDeleted();
                        //context.Database.Migrate();
                    }

                    await ApplicationDbContextSeed.SeedSampleDataAsync(context);

                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");
                    //var inner=ex.InnerException;
                    throw;
                }

            }
            await host.RunAsync();


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    webBuilder.UseUrls("http://0.0.0.0:3000", "https://0.0.0.0:3001");

                });
    }
}
