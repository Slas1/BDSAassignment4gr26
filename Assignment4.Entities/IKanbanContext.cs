using System;
using Microsoft.EntityFrameworkCore;

namespace Assignment4.Entities
{
    public interface IKanbanContext : IDisposable
    {
        DbSet<Tag> Tags { get; }
        DbSet<Task> Tasks { get; }
        DbSet<User> Users { get; }
        int SaveChanges();
    }
}