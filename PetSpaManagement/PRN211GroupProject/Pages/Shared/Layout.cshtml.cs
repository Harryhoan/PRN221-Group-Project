using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN211GroupProject.Pages
{
    public class LayoutModel : PageModel
    {
        public bool isLogin = false;
        public string name = string.Empty;
        public string email = string.Empty;
        public LayoutModel() { }

        void OnGet()
        {
            email = HttpContext.Session.GetString("Email");
            name = HttpContext.Session.GetString("Name");
                     isLogin = true;

    }

}
 
}