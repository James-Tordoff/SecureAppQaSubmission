using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SecureAppQa.Models;

namespace SecureAppQa.Pages.UserTickets
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly SecureAppQa.Models.SecureAppQaDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DeleteModel(SecureAppQa.Models.SecureAppQaDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
      public Ticket Ticket { get; set; } = default!;

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
                return RedirectToPage("/UserError", new { errorMessage = "Ticket Not Found. Please ensure you're logged in before accessing your tickets." });
            }
            else 
            {
                Ticket = ticket;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
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

            //var ticket = await _context.Tickets.FindAsync(id);
            var ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.Id == id && m.AspNetUserId == applicationUser.Id);

            if (ticket != null)
            {
                Ticket = ticket;
                _context.Tickets.Remove(Ticket);
                await _context.SaveChangesAsync();
            }
            else
            {
                return RedirectToPage("/UserError", new { errorMessage = "Ticket Not Found. Please ensure you're logged in before accessing your tickets." });
            }

            return RedirectToPage("./Index");
        }
    }
}
