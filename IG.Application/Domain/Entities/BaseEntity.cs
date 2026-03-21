using IG.Application.Domain.Interfaces;

namespace IG.Application.Domain.Entities
{
    public class BaseEntity<PK> : IBaseEntity<PK>
    {
        public PK Id { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public string? DeletedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
