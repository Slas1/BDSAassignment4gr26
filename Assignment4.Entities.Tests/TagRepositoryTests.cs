using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Xunit;
using Assignment4.Core;

namespace Assignment4.Entities.Tests
{
    public class TagRepositoryTests : IDisposable
    {
        private readonly KanbanContext _context;
        private readonly TagRepository _repo;

        public TagRepositoryTests(){
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<KanbanContext>();
            builder.UseSqlite(connection);
            var context = new KanbanContext(builder.Options);
            context.Database.EnsureCreated();

            //context.Tag.Add(new Tag {Name = "TaggyMcTag" });
            //context.SaveChanges();

            _context = context;
            _repo = new TagRepository(_context);
        }

        [Fact]
        public void Creates_Tag_and_returns_Response_And_ID(){
            //Arrange
            var tag = new TagCreateDTO{Name = "TaggyMissTag"};
            
            //Act
            var created = _repo.Create(tag);
            
            //Assert
            Assert.Equal((Response.Created, 1), created);
        }

        [Fact]
        public void Read_given_non_existing_id_returns_null()
        {
            var tag = _repo.Read(42);

            Assert.Null(tag);
        }

        [Fact]
        public void Read_given_existing_id_returns_tag()
        {
            var city = _repo.Read(1);

            Assert.Equal(new TagDTO(1, "TaggyMcTag"), city);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}