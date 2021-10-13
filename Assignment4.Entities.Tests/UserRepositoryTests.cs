using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Xunit;
using Assignment4.Core;

namespace Assignment4.Entities.Tests
{
    public class UserRepositoryTests : IDisposable
    {
        private readonly KanbanContext _context;
        private readonly UserRepository _repository;
        public UserRepositoryTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<KanbanContext>();
            builder.UseSqlite(connection);
            var context = new KanbanContext(builder.Options);
            context.Database.EnsureCreated();

            _context = context;
            _repository = new UserRepository(_context);
        }

        [Fact]
        public void Create_Given_User_Returns_CreatedResponse_And_Id()
        {
            var user = new UserCreateDTO
            {
                Name = "Sebastian", 
                Email = "Sebastian@gmail.com"
            };

            var created = _repository.Create(user);

            Assert.Equal((Response.Created, 1), created);
        }

        public void Read_Given_User_Returns_Null()
        {
            var user = _repository.Read(99);

            Assert.Null(user);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}