using System;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Lecture04.Entities;

namespace Assignment4
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = LoadConfiguration();
            var connectionString = configuration.GetConnectionString("Futurama");

            Console.WriteLine("Hello World!");
            
            var connection = new SqlConnection(connectionString);

            // var cmdText = "SELECT * FROM Users";

            var command = new SqlCommand(cmdText, connection);

            connection.Open();

            // open up command and get reader 
            var reader = command.ExecuteReader();

            connection.Close();
        }

        static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>(); // related to Program. Maps to Assignment4.csproj.

            return builder.Build();
        }

    }
}
