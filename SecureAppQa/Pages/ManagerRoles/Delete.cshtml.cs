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
    public class DeleteModel : PageModel
    {
        private readonly SecureAppQa.Models.SecureAppQaDbContext _context;

        public DeleteModel(SecureAppQa.Models.SecureAppQaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public AspNetRole AspNetRole { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.AspNetRoles == null)
            {
                return NotFound();
            }

            var aspnetrole = await _context.AspNetRoles.FirstOrDefaultAsync(m => m.Id == id);

            if (aspnetrole == null)
            {
                return NotFound();
            }
            else 
            {
                AspNetRole = aspnetrole;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.AspNetRoles == null)
            {
                return NotFound();
            }
            var aspnetrole = await _context.AspNetRoles.FindAsync(id);

            if (aspnetrole != null)
            {
                AspNetRole = aspnetrole;
                _context.AspNetRoles.Remove(AspNetRole);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
