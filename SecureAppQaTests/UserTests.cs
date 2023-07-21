using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SecureAppQa.Models;
using SecureAppQa.Services;
using System.ComponentModel;

namespace SecureAppQaTests
{
    public class UserTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            NUnit.Framework.Assert.Pass();
        }

        public SecureAppQaDbContext _dbcontext;
        private DbSet<Ticket> _dbSet;

        [Test]
        public void GetUsers()
        {
            _dbcontext = new SecureAppQaDbContext();
            var list = _dbcontext.AspNetUsers.ToList();

            NUnit.Framework.Assert.IsNotNull(list);
        }


        private Mock<IUserStore<IdentityUser>> _userStore;
        private IUserEmailStore<IdentityUser> _emailStore;
        private UserManager<IdentityUser> _userManager;

        [Test]
        [TestProperty("ExecutionOrder", "1")]
        public async Task CreateNewUser_Test()
        {

            //https://stackoverflow.com/questions/55412776/how-to-mock-usermanageridentityuser
            var store = new Mock<IUserStore<IdentityUser>>();
            store.Setup(x => x.FindByIdAsync("123", CancellationToken.None))
                .ReturnsAsync(new IdentityUser()
                {
                    UserName = "test@email.com",
                    Id = "123"
                });

            var mgr = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);

            //var mockUser = new Mock<UserManager<IdentityUser>>();
            var _emailStore = new Mock<IUserEmailStore<IdentityUser>>();
            var _userStore = new Mock<IUserStore<IdentityUser>>();



            bool createUsers = await CreateData.CreateUsers(mgr, _emailStore.Object, _userStore.Object);
            NUnit.Framework.Assert.True(createUsers);
        }

        [Test]
        public async Task AddUserToRoles_Test()
        {
            //Arrange - Go get your variables, whatever you need, your classes, functions etc

            // Arrange
            //https://stackoverflow.com/questions/55412776/how-to-mock-usermanageridentityuser
            //var store = new Mock<IUserStore<IdentityUser>>();
            //store.Setup(x => x.FindByIdAsync("123", CancellationToken.None))
            //    .ReturnsAsync(new IdentityUser()
            //    {
            //        UserName = "test@email.com",
            //        Id = "123"
            //    });

            //var mgr = new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);

            var mockUser = new Mock<UserManager<IdentityUser>>();

            //Act - Execute the function I want to test
            //Call the function you want to test here - giving the info from arrange into the Parameters of the function.

            bool addUserRoles = await CreateData.AddUsersToRoles(mockUser.Object);

            //Assert - See if the results we receive are the results I expect


            NUnit.Framework.Assert.True(addUserRoles);

            //return ffff;
        }

        [Test]
        public void GetSingleUsers()
        {
            _dbcontext = new SecureAppQaDbContext();
            var adminUser = _dbcontext.AspNetUsers.Where(o => o.Email == "admin@test.com");

            NUnit.Framework.Assert.IsNotNull(adminUser);
        }

        [Test]
        public async Task GetCustomerRoles() 
        {           
            _dbcontext = new SecureAppQaDbContext();

            var customerAccount = _dbcontext.AspNetUsers.Where(o => o.Email == "user1@test.com");
            var customerRoles = customerAccount.First().Roles;

            bool isCustomerInRole = customerRoles.Where(o => o.Name == "User").Any();

            NUnit.Framework.Assert.True(isCustomerInRole);
        }

        [Test]
        public void GetAdminRoles()
        {
            _dbcontext = new SecureAppQaDbContext();

            var adminAccount = _dbcontext.AspNetUsers.Where(o => o.Email == "admin@test.com");
            var adminRoles = adminAccount.First().Roles;

            bool isAdminInRole = adminRoles.Where(o => o.Name == "Admin").Any();

            NUnit.Framework.Assert.True(isAdminInRole);
        }
    
        [Test]
        public void TestingTemplate()
        {
            //Arrange - Go get your variables, whatever you need, your classes, functions etc


            //Act - Execute the function I want to test
            //Call the function you want to test here - giving the info from arrange into the Parameters of the function.

            //Assert - See if the results we receive are the results I expect
        }
    }
}