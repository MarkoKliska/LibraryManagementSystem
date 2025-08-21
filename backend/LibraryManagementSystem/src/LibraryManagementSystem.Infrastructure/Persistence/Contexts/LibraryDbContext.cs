using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace LibraryManagementSystem.Infrastructure.Persistence.Contexts;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Author> Authors { get; set; } = default!;
    public DbSet<Genre> Genres { get; set; } = default!;
    public DbSet<Book> Books { get; set; } = default!;
    public DbSet<BookCopy> BookCopies { get; set; } = default!;
    public DbSet<Rental> Rentals { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Library");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
