using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHMatsumotoDemo.Domain.Dtos
{
    public class EntityBaseDto
    {
        [NotMapped]
        public int Id { get; set; }
        public bool Enable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
