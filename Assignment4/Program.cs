using System;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Assignment4.Entities;

namespace Assignment4
{

    
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = LoadConfiguration();
            var connectionString = "Server=localhost;Database=Kanban;User Id=sa;Password=ea07a0f7-3ff7-4344-88c3-029d9f805fba"; //configuration.GetConnectionString("Kanban");

            var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>().UseSqlServer(connectionString);
            using var context = new KanbanContext(optionsBuilder.Options);
            
            KanbanContextFactory.Seed(context);
            // context.SaveChanges();
        }

        // Server=localhost;Database=Kanban;User Id=sa;Password=ea07a0f7-3ff7-4344-88c3-029d9f805fba

        static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>(); // related to Program. Maps to Assignment4.csproj.

            return builder.Build();
        }
        /* TODO:
            - Kunne indsætte data i databasen (få seed() til at virke)
            - Opgave 6
        */
    }
}
