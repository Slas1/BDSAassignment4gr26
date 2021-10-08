using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Assignment4.Entities;
using System.Collections.Generic;

namespace Assignment4 
{
    public class KanbanContextFactory : IDesignTimeDbContextFactory<KanbanContext>
    {
        public KanbanContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<Program>()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = "Server=localhost;Database=Kanban;User Id=sa;Password=ea07a0f7-3ff7-4344-88c3-029d9f805fba"; //configuration.GetConnectionString("Kanban");

            var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>()
                .UseSqlServer(connectionString);

            return new KanbanContext(optionsBuilder.Options);
        }

        public static void Seed(KanbanContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            
            // Creating users
            var User1 = new User { UserID = 1, Name = "User 1", Email = "user1@email.dk", Tasks = new List<Task>()};
            var User2 = new User { UserID = 2, Name = "User 2", Email = "user2@email.dk", Tasks = new List<Task>()};
            var User3 = new User { UserID = 3, Name = "User 3", Email = "user3@email.dk", Tasks = new List<Task>()};

            // Creating tags
            var foodTag = new Tag { TagID = 1, Name = "food", Tasks = new List<Task>()};
            var SchoolTag = new Tag { TagID = 2, Name = "School", Tasks = new List<Task>()};
            var FreeTimeTag = new Tag { TagID = 3, Name = "Free Time", Tasks = new List<Task>()};

            // Creating tasks
            var Task1 = new Task { Title= "Make homework", AssignedTo = User1, Description="thiefs is not always a funny task", State = State.Active, Tags = new List<Tag>(){foodTag}};
            var Task2 = new Task { Title= "Working out", AssignedTo = User3, Description="This is sooo good!", State = State.Active, Tags = new List<Tag>(){FreeTimeTag}};
            var Task3 = new Task { Title= "Cook dinner", Description="yessss", State = State.Resolved, Tags = new List<Tag>(){foodTag,FreeTimeTag}};
            
            // Adding tasks to tags
            foodTag.Tasks.Add(Task3);
            SchoolTag.Tasks.Add(Task1);
            FreeTimeTag.Tasks.AddRange(new List<Task>(){Task2, Task3});

            // Adding tasks to users
            User1.Tasks.Add(Task1);
            User3.Tasks.Add(Task2);
            
            context.Task.AddRange(new List<Task>(){Task1, Task2, Task3});
            context.Task.AddRange(new List<Task>(){Task1, Task2, Task3});

            context.SaveChanges();
        }
    }
}