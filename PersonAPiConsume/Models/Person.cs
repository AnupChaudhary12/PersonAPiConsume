using System.ComponentModel.DataAnnotations;

namespace PersonAPiConsume.Models
{
    public class Person
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string? firstName { get; set; }
        [Required]
        public string? lastName { get; set; }
        [Required]
        [EmailAddress]
        public string? email { get; set; }
    }
}
