using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations;

public class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.UserId)
            .IsRequired();
        builder.Property(r => r.BookCopyId)
            .IsRequired();
        builder.Property(r => r.RentalDate)
            .IsRequired();
        builder.Property(r => r.DueDate)
            .IsRequired();
        builder.Property(r => r.ReturnDate);

        builder.HasIndex(r => new { r.BookCopyId, r.ReturnDate });
    }
}
