using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecureAppQa.Pages
{
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class AuthorizeModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public AuthorizeModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}