using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SecureAppQa.Models;

namespace SecureAppQa.Pages.ManagerRoles
{
    public class IndexModel : PageModel
    {
        private readonly SecureAppQa.Models.SecureAppQaDbContext _context;

        public IndexModel(SecureAppQa.Models.SecureAppQaDbContext context)
        {
            _context = context;
        }

        public IList<AspNetRole> AspNetRole { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.AspNetRoles != null)
            {
                AspNetRole = await _context.AspNetRoles.ToListAsync();
            }
        }
    }
}
