using System.ComponentModel.DataAnnotations;

namespace PersonApiConsume.Models
{
    public class PersonApiOptions
    {
        [Required]
        public string BaseUrl { get; set; }
    }
}
