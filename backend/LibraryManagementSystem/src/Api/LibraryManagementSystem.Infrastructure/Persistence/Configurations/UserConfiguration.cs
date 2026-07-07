using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.FirstName)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(u => u.LastName)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(u => u.Email)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(u => u.PasswordHash)
            .IsRequired();
        builder.Property(u => u.IsDeleted)
            .IsRequired();
        builder.Property(u => u.Role)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasMany(u => u.Rentals)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
