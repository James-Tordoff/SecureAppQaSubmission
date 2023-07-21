using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SecureAppQa.Models;

namespace SecureAppQa.Pages.ManagerTickets
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly SecureAppQa.Models.SecureAppQaDbContext _context;

        public DetailsModel(SecureAppQa.Models.SecureAppQaDbContext context)
        {
            _context = context;
        }

      public Ticket Ticket { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }
            else 
            {
                Ticket = ticket;
                if (ticket.AspNetUser == null && ticket.AspNetUserId != null)
                {
                    //If for some readon the user cant be found but theres an ID - search them up

                    AspNetUser findUser = _context.AspNetUsers.Find(ticket.AspNetUserId);
                    Ticket.AspNetUser = findUser;

                }
            }
            return Page();
        }
    }
}
