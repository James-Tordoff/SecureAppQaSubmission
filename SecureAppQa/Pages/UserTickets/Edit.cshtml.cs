using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecureAppQa.Models;
using static SecureAppQa.Pages.UserTickets.CreateModel;

namespace SecureAppQa.Pages.UserTickets
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly SecureAppQa.Models.SecureAppQaDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(SecureAppQa.Models.SecureAppQaDbContext context, UserManager<IdentityUser> userManager)
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


            //var ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.Id == id);
            var ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.Id == id && m.AspNetUserId == applicationUser.Id);
            if (ticket == null)
            {
                return RedirectToPage("/UserError", new { errorMessage = "Ticket Not Found. Please ensure you're logged in before accessing your tickets." });
                //return NotFound();
            }
            Ticket = ticket;
            

            return Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            IdentityUser applicationUser = await _userManager.GetUserAsync(User);
            if (applicationUser == null)
            {
                return RedirectToPage("/UserError", new { errorMessage = "User Not Found. Please ensure you're logged in before accessing your tickets." });
            }

            if (Ticket.AspNetUserId != applicationUser.Id)
            {
                return RedirectToPage("/UserError", new { errorMessage = "Ticket not found againt this user. Please ensure you're logged in before accessing your tickets." });
            }

            _context.Attach(Ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(Ticket.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TicketExists(string id)
        {
          return (_context.Tickets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
