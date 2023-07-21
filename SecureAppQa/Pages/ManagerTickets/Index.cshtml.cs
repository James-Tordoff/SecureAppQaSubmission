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
    public class IndexModel : PageModel
    {
        private readonly SecureAppQa.Models.SecureAppQaDbContext _context;

        public IndexModel(SecureAppQa.Models.SecureAppQaDbContext context)
        {
            _context = context;
        }

        public IList<Ticket> Ticket { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Tickets != null)
            {
                Ticket = await _context.Tickets
                .Include(t => t.AspNetUser).ToListAsync();
            }
        }
    }
}
