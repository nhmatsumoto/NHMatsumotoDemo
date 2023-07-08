using System.ComponentModel.DataAnnotations;

namespace NHMatsumotoDemo.Domain.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
        public bool Enable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
