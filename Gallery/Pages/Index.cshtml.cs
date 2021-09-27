using Gallery.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gallery.Pages
{
    public class IndexModel : PageModel
    {
        public CreateImage Command { get; set; }

        public void OnGet()
        {
        }
    }
}
