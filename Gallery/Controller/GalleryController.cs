using System;
using System.IO;
using System.Linq;
using Gallery.Data;
using Gallery.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Gallery.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly GalleryContext _context;

        public GalleryController(GalleryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetImages()
        {
            var gallery = _context.Galleries.ToList();

            return Ok(gallery);
        }

        [HttpPost]
        public IActionResult CreateImage([FromForm] CreateImage command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var random = new Random();
            var imgName = $"{random.Next(1000, 100000)}{Path.GetExtension(command.Image.FileName)}";
            var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","img", imgName);

            using var stream = new FileStream(path, FileMode.Create);
            command.Image.CopyTo(stream);


            var gallery = new Model.Gallery(command.Name,imgName,command.Category);

            _context.Galleries.Add(gallery);
            _context.SaveChanges();

            return Ok(gallery);
        }
    }
}