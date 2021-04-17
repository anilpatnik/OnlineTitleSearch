using System.ComponentModel.DataAnnotations;

namespace OTS.Models
{
    public class Lookup
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
