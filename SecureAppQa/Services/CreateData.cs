using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using SecureAppQa.Models;
using System.Text;

namespace SecureAppQa.Services
{
    
    public class CreateData
    {

        public static async Task<bool> AddUsersToRoles(UserManager<IdentityUser> _userManager)
        {
            try
            {
                var user1 = await _userManager.FindByEmailAsync("user1@test.com");
                var addtoUserRoleResult = await _userManager.AddToRoleAsync(user1, "User");
                if (!addtoUserRoleResult.Succeeded)
                {
                    //Failed to add user to role.
                    return false;
                }

                var user2 = await _userManager.FindByEmailAsync("user2@test.com");
                addtoUserRoleResult = await _userManager.AddToRoleAsync(user2, "User");
                if (!addtoUserRoleResult.Succeeded)
                {
                    //Failed to add user to role.
                    return false;
                }

                var user3 = await _userManager.FindByEmailAsync("admin@test.com");
                addtoUserRoleResult = await _userManager.AddToRoleAsync(user3, "User");
                var addtoAdminRoleResult = await _userManager.AddToRoleAsync(user3, "Admin");
                if (!addtoUserRoleResult.Succeeded || !addtoAdminRoleResult.Succeeded)
                {
                    //Failed to add user to role.
                    return false;
                }
            }
            catch
            {
                // Something went wrong.
                return false;
            }            

            //Users were assigned to new roles
            return true;
        }

        public static async Task<bool> CreateRoles(RoleManager<IdentityRole> _roleManager)
        {
            try
            {
                //Create User Role
                var role = new IdentityRole();
                role.Name = "User";
                await _roleManager.CreateAsync(role);

                //Create Admin Role
                var role2 = new IdentityRole();
                role2.Name = "Admin";
                await _roleManager.CreateAsync(role2);
            }
            catch (Exception ex)
            {
                // Something went wrong.
                return false;
            }

            // Roles Created ok
            return true;
        }

        public static async Task<bool> CreateUsers(UserManager<IdentityUser> _userManager, IUserEmailStore<IdentityUser> _emailStore, IUserStore<IdentityUser> _userStore)
        {
            try
            {
                // Create 1st User
                var EmailAddress = "user1@test.com";
                var Password = "Password.1";
                var user = CreateUser();
                var registerUser = await RegisterAccount(_userManager, _emailStore, _userStore, user, EmailAddress, Password);
                if (!registerUser)
                {
                    //Failed to create user.
                    return false;
                }

                // Confirm User Account to allow login with out email confirmation.
                var userAccountconfirmed = await ConfirmUserAccount(_userManager, user);
                if (!userAccountconfirmed)
                {
                    //Failed to Confirm User account.
                    return false;
                }

                // Create 2nd User
                EmailAddress = "user2@test.com";
                Password = "Password.1";
                var user2 = CreateUser();
                registerUser = await RegisterAccount(_userManager, _emailStore, _userStore, user2, EmailAddress, Password);
                if (!registerUser)
                {
                    //Failed to create user.
                    return false;
                }

                // Confirm User Account to allow login with out email confirmation.
                var userAccountconfirmed2 = await ConfirmUserAccount(_userManager, user2);
                if (!userAccountconfirmed)
                {
                    //Failed to Confirm User account.
                    return false;
                }

                // Create 3rd User/Admin
                EmailAddress = "admin@test.com";
                Password = "Password.1";
                var user3 = CreateUser();
                registerUser = await RegisterAccount(_userManager, _emailStore, _userStore, user3, EmailAddress, Password);
                if (!registerUser)
                {
                    //Failed to create user.
                    return false;
                }

                // Confirm User Account to allow login with out email confirmation.
                var userAccountconfirmed3 = await ConfirmUserAccount(_userManager, user3);
                if (!userAccountconfirmed3)
                {
                    //Failed to Confirm User account.
                    return false;
                }
            }
            catch
            {
                // Something went wrong.
                return false;
            }            

            // Both user accounts created successfully.
            return true;
        }

        public static async Task<bool> ConfirmUserAccount(UserManager<IdentityUser> _userManager, IdentityUser user)
        {
            try
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                var result = await _userManager.ConfirmEmailAsync(user, code);
                if (!result.Succeeded)
                {
                    // Account confirmation failed.
                    return false;
                }

            }
            catch
            {
                //Something went wrong
                return false;
            }

            // Account confirm ok
            return true;
        }

        public static async Task<bool> RegisterAccount(UserManager<IdentityUser> _userManager, IUserEmailStore<IdentityUser> _emailStore, IUserStore<IdentityUser> _userStore,IdentityUser user, string EmailAddress, string Password)
        {
            try
            {
                await _userStore.SetUserNameAsync(user, EmailAddress, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, EmailAddress, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Password);
                if (!result.Succeeded)
                {
                    // Account creation failed.
                    return false;
                }
            } catch
            {
                // Something went wrong.
                return false; 
            }

            // Account created all ok
            return true;
        }

        public static async Task<bool> CreateTickets(UserManager<IdentityUser> _userManager)
        {
            try
            {              

                var user = await _userManager.FindByEmailAsync("user1@test.com");
                var user2 = await _userManager.FindByEmailAsync("user2@test.com");


                // Get users first via hard coded email address, then create tickets.
                Ticket ticket1 = new Ticket() 
                {                    
                    AspNetUserId = user.Id,
                    Subject = "Help! My Screen is upside-down",
                    Description = "It's like I'm living in Australia."                                     
                };

                Ticket ticket2 = new Ticket()
                {                 
                    AspNetUserId = user.Id,
                    Subject = "Phone won't pair to our vehicle.",
                    Description = "When using the app, I cannot control the heating with my phone."
                };

                Ticket ticket3 = new Ticket()
                {                    
                    AspNetUserId = user.Id,
                    Subject = "Payments not being taken",
                    Description = "I Can't take a payment for a new subscription."
                };

                Ticket ticket4 = new Ticket()
                {
                    AspNetUserId = user2.Id,
                    Subject = "I dont like to make tickets.",
                    Description = "But I am struggling to do anything so I made a ticket."
                };



                SecureAppQaDbContext secureAppQaDbContext = new SecureAppQaDbContext();
                secureAppQaDbContext.Tickets.Add(ticket1);
                secureAppQaDbContext.Tickets.Add(ticket2);
                secureAppQaDbContext.Tickets.Add(ticket3);
                secureAppQaDbContext.Tickets.Add(ticket4);

                await secureAppQaDbContext.SaveChangesAsync();

            }
            catch
            { 
                //Something went wrong.
                return false;
            }

            //All tickets created.
            return true;
        }


        private static IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

    }
}
