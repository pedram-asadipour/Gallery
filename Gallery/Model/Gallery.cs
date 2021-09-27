using System.ComponentModel.DataAnnotations;

namespace Gallery.Model
{
    public class Gallery
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; private set; }

        [Required]
        [MaxLength(500)]
        public string Image { get; private set; }

        [Required]
        [MaxLength(200)]
        public string Category { get; private set; }

        public Gallery(string name, string image, string category)
        {
            Name = name;
            Image = image;
            Category = category;
        }
    }
}
