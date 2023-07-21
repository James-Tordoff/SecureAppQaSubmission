using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SecureAppQa.Models;

namespace SecureAppQa.Pages.ManagerRoles
{
    public class CreateModel : PageModel
    {
        private readonly SecureAppQa.Models.SecureAppQaDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateModel(SecureAppQa.Models.SecureAppQaDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public class AspNetRoleCreate
        {
            public string Name { get; set; } = null!;
        }

        [BindProperty]
        public AspNetRoleCreate AspNetRoleCreateVM { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.AspNetRoles == null || AspNetRoleCreateVM == null)
            {
                return Page();
            }
            var role = new IdentityRole();
            role.Name = AspNetRoleCreateVM.Name;
            await _roleManager.CreateAsync(role);

            return RedirectToPage("./Index");
        }
    }
}
