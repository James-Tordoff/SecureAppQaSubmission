using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SecureAppQa.Models;

namespace SecureAppQa.Pages.UserTickets
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly SecureAppQa.Models.SecureAppQaDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(SecureAppQa.Models.SecureAppQaDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            IdentityUser applicationUser = _userManager.GetUserAsync(User).Result;
            if (applicationUser == null)
            {
                return RedirectToPage("/UserError", new { errorMessage = "You must be logged in to submit a new ticket." });
            }
            return Page();
        }

        public partial class TicketVM
        {
            [MinLength(10, ErrorMessage = "Subject must be 10 - 150 characters")]
            [MaxLength(150, ErrorMessage = "Subject must be 10 - 150 characters")]

            public string Subject { get; set; } = null!;

            [MinLength(10, ErrorMessage = "Description must be 10 - 350 characters")]
            [MaxLength(350, ErrorMessage = "Description must be 10 - 350 characters")]

            public string Description { get; set; } = null!;
        }

        [BindProperty]
        public TicketVM Ticket { get; set; } = new TicketVM();

        //public string ErrorMessage = "";

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            IdentityUser applicationUser = await _userManager.GetUserAsync(User);
            if (applicationUser == null)
            {
                return RedirectToPage("/UserError", new { errorMessage = "You must be logged in to submit a new ticket." });
            }


            if (!ModelState.IsValid || _context.Tickets == null || Ticket == null)
            {
                return Page();
            }

            Ticket NewTicket = new Ticket();
            NewTicket.Subject = Ticket.Subject;
            NewTicket.Description = Ticket.Description;
            NewTicket.AspNetUserId = applicationUser.Id;

            _context.Tickets.Add(NewTicket);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
