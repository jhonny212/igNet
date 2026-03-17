using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configuration
{
    public static class BaseEntityConfiguration
    {
        public static void ConfigureAuditEntity<T, PK>(this EntityTypeBuilder<T> builder)
            where T : BaseEntity<PK>
        {
            builder.Property(prop => prop.CreatedBy).HasColumnType("varchar(70)");
            builder.Property(prop => prop.DeletedBy).HasColumnType("varchar(70)");
            builder.Property(prop => prop.UpdatedBy).HasColumnType("varchar(70)");
            builder.Property(prop => prop.CreatedAt).HasColumnType("datetime").IsRequired(true);
            builder.Property(prop => prop.DeletedAt).HasColumnType("datetime").IsRequired(false);
            builder.Property(prop => prop.UpdatedAt).HasColumnType("datetime").IsRequired(false);
        }
    }
}
