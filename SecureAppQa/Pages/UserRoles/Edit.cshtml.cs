using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SecureAppQa.Pages.UserRoles
{
    public class EditModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SecureAppQa.Models.SecureAppQaDbContext _context;

        public EditModel(SecureAppQa.Models.SecureAppQaDbContext context,  UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
        }
        public class UserRolesEditVM
        {
            [Required]
            [StringLength(450)]
            [Display(Name = "User Name")]
            public string? UserId { get; set; }
            [Display(Name = "User Name")]
            public string? UserName { get; set; }
        }

        [BindProperty]
        public List<string> RemoveRoles { get; set; }
        [BindProperty]
        public List<string> AddRoles { get; set; }
        [BindProperty]
        public UserRolesEditVM _UserRolesEditVM { get; set; } = default!;
        public List<SelectListItem> TenantRoleList { get; set; } = default!;
        public List<SelectListItem> TenantUserRoleList { get; set; } = default!;
        public List<SelectListItem> TenantUserRolesAvailableList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.AspNetUsers == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            // Return to view Current list of Users Assigned Roles
            
            var roles = await _userManager.GetRolesAsync(user);

            TenantUserRoleList = new List<SelectListItem>();

            foreach (var role in roles)
            {
                TenantUserRoleList.Add(new SelectListItem { Text = role });
            }

            // Return to view Current list of Users Assigned Roles
            var RoleList = await _roleManager.Roles.ToListAsync();

            TenantRoleList = RoleList.OrderBy(m => m.Name).ToList().Select(mm => new SelectListItem
            {
                Text = mm.Name,
            }).ToList();


            // Remove duplicates from lists so we have a final list of available roles that the user is not yet assigned to.
            TenantUserRolesAvailableList = TenantRoleList.Where(m => !TenantUserRoleList.Any(z => z.Text == m.Text)).ToList();

            _UserRolesEditVM = new UserRolesEditVM();
            _UserRolesEditVM.UserId = id;
            _UserRolesEditVM.UserName = user.UserName;

            AddRoles = new List<string>();
            RemoveRoles = new List<string>();

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
            var user = await _userManager.FindByIdAsync(_UserRolesEditVM.UserId);
            //var aspNetUser = await _context.AspNetUsers.FirstOrDefaultAsync(m => m.Id == UserRolesEditVM.UserId);

            if (user == null)
            {
                return NotFound();
            }

            var test = RemoveRoles;

            if (RemoveRoles.Any())
            {
                foreach (var role in RemoveRoles)
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, role);
                }
            }
            if (AddRoles.Any())
            {
                //IEnumerable<string> roles = AddRoles;
                //var result = await _userManager.AddToRolesAsync(user, roles);
                foreach (var role in AddRoles)
                {
                    try
                    {
                        var result = await _userManager.AddToRoleAsync(user, role);
                    }
                    catch (Exception exp)
                    {
                        var message = exp.Message;
                    }

                }
            }

            return RedirectToPage("./Index");
        }
    }
}
