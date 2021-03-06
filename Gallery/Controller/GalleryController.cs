using System;
using System.IO;
using System.Linq;
using Gallery.Data;
using Gallery.Model;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                var gallery = _context.Galleries.ToList();
                return Ok(gallery);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NoContent();
            }
        }

        [HttpPost]
        public IActionResult CreateImage([FromForm] CreateImage command)
        {
            var filePath = "";

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var random = new Random();
                var imgName = $"{command.Name}-{random.Next(1000, 100000)}-{random.Next(1, 100000)}{Path.GetExtension(command.Image.FileName)}";
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","img");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                filePath = Path.Combine(path, imgName);
                
                using var stream = new FileStream(filePath, FileMode.Create);
                command.Image.CopyTo(stream);

                var gallery = new Model.Gallery(command.Name,imgName,command.Category);

                _context.Galleries.Add(gallery);
                _context.SaveChanges();

                return Ok(gallery);
            }
            catch (Exception e)
            {
                if(System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                Console.WriteLine(e);
                return StatusCode(417);
            }
        }
    }
}