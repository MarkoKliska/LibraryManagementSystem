using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations;

public class BookCopyConfiguration : IEntityTypeConfiguration<BookCopy>
{
    public void Configure(EntityTypeBuilder<BookCopy> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.BookId)
            .IsRequired();
        builder.Property(c => c.IsDeleted)
            .IsRequired();

        builder.HasMany(c => c.Rentals)
            .WithOne(r => r.BookCopy)
            .HasForeignKey(r => r.BookCopyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
