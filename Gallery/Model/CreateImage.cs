using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Gallery.Model
{
    public class CreateImage
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        [MaxLength(200)]
        public string Category { get; set; }
    }
}