using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using SecureAppQa.Models;

namespace SecureAppQa.Pages.UserRoles
{
    public class IndexModel : PageModel
    {
        private readonly SecureAppQa.Models.SecureAppQaDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(SecureAppQa.Models.SecureAppQaDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<AspNetUser> AspNetUser { get; set; } = default!;
        public DataTable Dt { get; set; } = default!;

        public IList<UserRoleModel> UserRoleModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.AspNetUsers != null)
            {
                var UserList = await _userManager.Users.ToListAsync();

                int roleCount = 0;
                UserRoleModel = new List<UserRoleModel>();  
                foreach (var user in UserList)
                {
                    roleCount = _userManager.GetRolesAsync(user).Result.Count();

                    UserRoleModel userRoleModel = new UserRoleModel();

                    userRoleModel.UserName = user.UserName;
                    userRoleModel.RoleCount = roleCount;
                    userRoleModel.EmailConfirmed = user.EmailConfirmed;
                    userRoleModel.Id = user.Id;


                    UserRoleModel.Add(userRoleModel);
                }

                //UserRoleModel = await _context.UserRoleModel.ToListAsync();
            }
        }
    }
}
