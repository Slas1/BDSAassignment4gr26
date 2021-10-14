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

            context.User.AddRange(
                new User{UserID = 1, Name = "Sebastian", Email = "Sebastian@gmail.com"},
                new User{UserID = 2, Name = "Alexander", Email = "Alexander@gmail.com"},
                new User{UserID = 3, Name = "Tessa", Email = "Tessa@gmail.com"},
                new User{UserID = 4, Name = "Swagger", Email = "Swagger@gmail.com"}
            );

            context.SaveChanges();

            _context = context;
            _repository = new UserRepository(_context);
        }

        [Fact]
        public void Create_given_user_returns_createdResponse_and_id()
        {            
            var user = new UserCreateDTO
            {
                Name = "Rasmus", 
                Email = "Rasmus@gmail.com"
            };

            var created = _repository.Create(user);

            Assert.Equal((Response.Created, 5), created);
        }

        /*
        [Fact]
        public void Create_user_with_existing_email_returns_conflict_response()
        {            
            var user = new UserCreateDTO
            {
                Name = "Alexander", 
                Email = "Alexander@gmail.com"
            };

            var created = _repository.Create(user);

            Assert.Equal((Response.Conflict,2), created);
        }*/

        [Fact]
        public void Delete_existing_user_returns_deleted_response()
        {            
            var response = _repository.Delete(2, false);

            Assert.Equal(Response.Deleted, response);
        }

        [Fact]
        public void Delete_non_existing_user_returns_notfound_response()
        {            
            var response = _repository.Delete(99);

            Assert.Equal(Response.NotFound, response);
        }


        [Fact]
        public void Read_non_existing_user_returns_null()
        {
            var user = _repository.Read(99);

            Assert.Null(user);
        }

        [Fact]
        public void Read_returns_user_with_id_2()
        {
            var user = _repository.Read(2);

            Assert.Equal(2, user.Id);
            Assert.Equal("Alexander", user.Name);
            Assert.Equal("Alexander@gmail.com", user.Email);
        }

        [Fact]
        public void ReadAll_returns_all_users()
        {
            var characters = _repository.ReadAll();

            Assert.Collection(characters,
                c => Assert.Equal(new UserDTO(1, "Sebastian", "Sebastian@gmail.com"), c),
                c => Assert.Equal(new UserDTO(2, "Alexander", "Alexander@gmail.com"), c),
                c => Assert.Equal(new UserDTO(3, "Tessa", "Tessa@gmail.com"), c),
                c => Assert.Equal(new UserDTO(4, "Swagger", "Swagger@gmail.com"), c)
            );
        }

        [Fact]
        public void Update_non_existing_user_returns_notFound_response()
        {
            var user = new UserUpdateDTO
            {
                Name = "Mads", 
                Email = "Mads@gmail.com",
                Id = 6 
            };
            
            var response = _repository.Update(user);

            Assert.Equal(Response.NotFound, response);
        }

        [Fact]
        public void Update_existing_user_returns_updated_response()
        {
            var user = new UserUpdateDTO
            {
                Name = "Mads", 
                Email = "Mads@gmail.com",
                Id = 2 
            };
            
            var response = _repository.Update(user);

            Assert.Equal(Response.Updated, response);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}