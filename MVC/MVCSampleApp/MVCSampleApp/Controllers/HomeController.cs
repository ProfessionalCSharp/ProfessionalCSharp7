using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MVCSampleApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public string Hello() => "Hello, ASP.NET Core MVC";

        public string Greeting(string name) =>
            HtmlEncoder.Default.Encode($"Hello, {name}");

        public string Greeting2(string id) =>
            HtmlEncoder.Default.Encode($"Hello, {id}");

        public int Add(int x, int y) => x + y;
    }
}
