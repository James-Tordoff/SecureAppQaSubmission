using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecureAppQa.Pages
{
    public class UserErrorModel : PageModel
    {
        public string ErrorMessage = "";

        public void OnGet(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
