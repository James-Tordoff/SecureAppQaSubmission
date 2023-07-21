using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SecureAppQa.Areas.Identity.Pages.Account;
using SecureAppQa.Models;

namespace SecureAppQa.Data
{
    //public class DataCreation
    //{
    //    private readonly SignInManager<IdentityUser> _signInManager;
    //    private readonly UserManager<IdentityUser> _userManager;
    //    private readonly IUserStore<IdentityUser> _userStore;
    //    private readonly IUserEmailStore<IdentityUser> _emailStore;
    //    private readonly ILogger<RegisterModel> _logger;
    //    private readonly IEmailSender _emailSender;

    //    public DataCreation(
    //      UserManager<IdentityUser> userManager,
    //      IUserStore<IdentityUser> userStore,
    //      SignInManager<IdentityUser> signInManager,
    //      ILogger<RegisterModel> logger,
    //      IEmailSender emailSender)
    //    {
    //        _userManager = userManager;
    //        _userStore = userStore;
    //        //_emailStore = GetEmailStore();
    //        _signInManager = signInManager;
    //        _logger = logger;
    //        _emailSender = emailSender;
    //    }

    //    public static async void DataSetUpAsync()
    //    {
    //        CreateRoles();

    //        RegisterModel registerModel; // = new RegisterModel(_userManager, _userStore, _signInManager, _logger, _emailSender);
            
    //        registerModel.CreateNewUser("test@test.com", "Password.1");

    //        //using (SecureAppQaDbContext context = new SecureAppQaDbContext())
    //        //{


    //        //    var hello = 0;

    //        //    Ticket NewTicket = new Ticket();
    //        //    NewTicket.Subject = "Help!";
    //        //    NewTicket.Description = "";
    //        //    NewTicket.AspNetUserId = "";

    //        //    context.Tickets.Add(NewTicket);
    //        //    await context.SaveChangesAsync();
    //        //}
    //    }

    //    public static async void CreateRoles() 
    //    {
    //        using (SecureAppQaDbContext context = new SecureAppQaDbContext())
    //        {
    //            AspNetRole adminRole = new AspNetRole()
    //            {
    //                Id = Guid.NewGuid().ToString(),
    //                Name = "Admin",
    //            };

    //            AspNetRole userRole = new AspNetRole()
    //            {
    //                Id = Guid.NewGuid().ToString(),
    //                Name = "User",
    //            };

    //            context.AspNetRoles.Add(adminRole);
    //            context.AspNetRoles.Add(userRole);
    //            await context.SaveChangesAsync();
    //        }
    //    }      
    //}

}
