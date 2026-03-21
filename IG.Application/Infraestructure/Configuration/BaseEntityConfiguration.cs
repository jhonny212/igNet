using IG.Application.Domain.Entities;
using IG.Application.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IG.Application.Infraestructure.Configuration
{
    public static class BaseEntityConfiguration
    {
        public static void ConfigureAuditEntity<T, PK>(this EntityTypeBuilder<T> builder)
            where T : class, IBaseEntity<PK>
        {
            builder.Property(prop => prop.CreatedBy).HasMaxLength(70);
            builder.Property(prop => prop.DeletedBy).HasMaxLength(70);
            builder.Property(prop => prop.UpdatedBy).HasMaxLength(70);
            builder.Property(prop => prop.CreatedAt).IsRequired(true);
            builder.Property(prop => prop.DeletedAt).IsRequired(false);
            builder.Property(prop => prop.UpdatedAt).IsRequired(false);
        }
    }
}
