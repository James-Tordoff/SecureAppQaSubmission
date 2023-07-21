using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureAppQa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureAppQaTests
{
    public class TicketTests
    {
        public SecureAppQaDbContext _dbcontext;
        private DbSet<Ticket> _dbSet;

        [Test]
        public void GetTickets()
        {
            _dbcontext = new SecureAppQaDbContext();
            var list = _dbcontext.Tickets.ToList();

            NUnit.Framework.Assert.IsNotNull(list);
        }

        [Test]
        [TestProperty("ExecutionOrder", "1")]
        public void AddNewTicket()
        {
            _dbcontext = new SecureAppQaDbContext();
            AspNetUser user = _dbcontext.AspNetUsers.First();
            Ticket ticket = new Ticket()
            {
                AspNetUserId = user.Id,
                DateCreated = DateTime.UtcNow,
                Description = "Test Desc",
                Subject = "Test Subject",
                IsActive = true,
            };

            _dbcontext.Tickets.Add(ticket);
            var result = _dbcontext.SaveChanges();

            NUnit.Framework.Assert.True(result == 1);
        }

        [Test]
        [TestProperty("ExecutionOrder", "2")]
        public void GetTicketDetails()
        {
            _dbcontext = new SecureAppQaDbContext();

            Ticket myTicket = _dbcontext.Tickets.Where(o => o.Subject == "Test Subject").Last();


            NUnit.Framework.Assert.True(myTicket != null);
        }

        [Test]
        [TestProperty("ExecutionOrder", "3")]
        public void EditTicket()
        {
            _dbcontext = new SecureAppQaDbContext();
            AspNetUser user = _dbcontext.AspNetUsers.First();
            //Ticket myTicket = _dbcontext.Tickets.Find("9bb1449b-668a-4ae4-a270-bd0b2606a62e");
            Ticket myTicket = _dbcontext.Tickets.OrderBy(o => o.DateCreated.ToString()).Last();
            myTicket.Subject = "I changed this";

            _dbcontext.Entry(myTicket).State = EntityState.Modified;
            var result = _dbcontext.SaveChanges();

            NUnit.Framework.Assert.True(result == 1);
        }

        [Test]
        [TestProperty("ExecutionOrder", "4")]
        public void DeleteTicket()
        {

            string guid = new Guid().ToString();
            _dbcontext = new SecureAppQaDbContext();

            //Add a new Ticket with a GUID
            _dbcontext = new SecureAppQaDbContext();
            AspNetUser user = _dbcontext.AspNetUsers.First();
            Ticket ticket = new Ticket()
            {
                AspNetUserId = user.Id,
                DateCreated = DateTime.UtcNow,
                Description = "Test Desc",
                Subject = guid,
                IsActive = true,
            };

            _dbcontext.Tickets.Add(ticket);
            var addResult = _dbcontext.SaveChanges();


            var ticketToDelete = _dbcontext.Tickets.Where(o => o.Subject == guid).First();


            _dbcontext.Tickets.Remove(ticketToDelete);
            var deleteResult = _dbcontext.SaveChanges();

            NUnit.Framework.Assert.True(deleteResult == 1);
        }
    }
}
