using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesSample.Pages
{
    public class PageWithParameterModel : PageModel
    {
        public void OnGet(int id = 0)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}