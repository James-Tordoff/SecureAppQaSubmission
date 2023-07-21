using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using SecureAppQa.Models;

namespace SecureAppQa.Pages.UserTickets
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly SecureAppQa.Models.SecureAppQaDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(SecureAppQa.Models.SecureAppQaDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

      public Ticket Ticket { get; set; } = default!;

        public string ErrorMessage = "";

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            IdentityUser applicationUser = await _userManager.GetUserAsync(User);
            if (applicationUser == null)
            {
                return RedirectToPage("/UserError", new { errorMessage = "User Not Found. Please ensure you're logged in before accessing your tickets." });
            }

            var ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.Id == id && m.AspNetUserId == applicationUser.Id);   

            if (ticket == null)
            {               
                return RedirectToPage("/UserError", new { errorMessage = "Unable to access ticket." });
            }
            else 
            {
                Ticket = ticket;
            }
            return Page();
        }
    }
}
