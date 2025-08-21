using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Title)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(b => b.AuthorId)
            .IsRequired();
        builder.Property(b => b.GenreId)
            .IsRequired();
        builder.Property(b => b.Isbn13)
            .HasMaxLength(13)
            .IsRequired();
        builder.Property(b => b.IsDeleted)
            .IsRequired();

        builder.HasIndex(b => b.Isbn13)
            .IsUnique();

        builder.HasMany(b => b.Copies)
            .WithOne(c => c.Book)
            .HasForeignKey(c => c.BookId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}