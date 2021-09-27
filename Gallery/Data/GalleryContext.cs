using Microsoft.EntityFrameworkCore;

namespace Gallery.Data
{
    public class GalleryContext : DbContext
    {
        public GalleryContext(DbContextOptions<GalleryContext> options) : base(options)
        {
        }

        public DbSet<Model.Gallery> Galleries { get; set; }

    }
}
