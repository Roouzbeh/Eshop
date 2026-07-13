using System.ComponentModel.DataAnnotations;

namespace IDP.Domain.Entities.BaseEntities
{
    public class BaseEntity 
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
    }
}
