using System;
using Assignment4.Entities;

namespace Assignment4.Entities.Tests
{
    public class TagRepositoryTests
    {
        private readonly KanbanContext _context;
        private readonly TagRepository _repo;

        public TagRepositoryTests(){
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<KanbanContext>();
            builder.UseSqlite(connection);
            var context = new ComicsContext(builder.Options);
            context.Database.EnsureCreated();
            context.Cities.Add(new tag { Name = "TaggyMcTag" });
            context.SaveChanges();

            _context = context;
            _repo = new CityRepository(_context);
        }

        [Fact]
        public void Creates_tag_and_returns_it(){
            //Arrange
            var tag = new TagCreateDTO("TaggyMissTag");

            //Act
            var created = _repo.Create(tag)
            
            //Assert
            Assert.Equal(new TagDTO(2, "TaggyMissTag"), created);
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