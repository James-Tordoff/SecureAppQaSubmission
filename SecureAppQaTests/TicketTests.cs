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
            //Arrange
            _dbcontext = new SecureAppQaDbContext();

            //Act
            var list = _dbcontext.Tickets.ToList();

            //Assert
            NUnit.Framework.Assert.IsNotNull(list);
        }

        [Test]
        [TestProperty("ExecutionOrder", "1")]
        public void AddNewTicket()
        {
            //Arrange
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

            //Act
            _dbcontext.Tickets.Add(ticket);
            var result = _dbcontext.SaveChanges();

            //Assert
            NUnit.Framework.Assert.True(result == 1);
        }

        [Test]
        [TestProperty("ExecutionOrder", "2")]
        public void GetTicketDetails()
        {
            //Arrange
            _dbcontext = new SecureAppQaDbContext();

            //Act
            Ticket myTicket = _dbcontext.Tickets.Where(o => o.Subject == "Test Subject").Last();

            //Assert
            NUnit.Framework.Assert.True(myTicket != null);
        }

        [Test]
        [TestProperty("ExecutionOrder", "3")]
        public void EditTicket()
        {
            //Arrange
            _dbcontext = new SecureAppQaDbContext();
            AspNetUser user = _dbcontext.AspNetUsers.First();            
            Ticket myTicket = _dbcontext.Tickets.OrderBy(o => o.DateCreated.ToString()).Last();
            myTicket.Subject = "I changed this";

            //Act
            _dbcontext.Entry(myTicket).State = EntityState.Modified;
            var result = _dbcontext.SaveChanges();

            //Assert
            NUnit.Framework.Assert.True(result == 1);
        }

        [Test]
        [TestProperty("ExecutionOrder", "4")]
        public void DeleteTicket()
        {
            //Arrange
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

            //Act
            _dbcontext.Tickets.Add(ticket);
            var addResult = _dbcontext.SaveChanges();


            var ticketToDelete = _dbcontext.Tickets.Where(o => o.Subject == guid).First();


            _dbcontext.Tickets.Remove(ticketToDelete);
            var deleteResult = _dbcontext.SaveChanges();

            //Assert
            NUnit.Framework.Assert.True(deleteResult == 1);
        }

        [Test]        
        public void TryAddBadTicketNoUser()
        {
            //In our Database - User, Subject, and Desc are all NOT NULL.
            //Attempting to add a record with null values here should fail.

            //Arrange
            _dbcontext = new SecureAppQaDbContext();
            AspNetUser user = _dbcontext.AspNetUsers.First();
            Ticket ticket = new Ticket()
            {
                AspNetUserId = null,
                DateCreated = DateTime.UtcNow,
                Description = "Description",
                Subject = "Subject",
                IsActive = true,
            };

            //Act
            int? result = null;
            try
            {
                _dbcontext.Tickets.Add(ticket);
                result = _dbcontext.SaveChanges();
            }
            catch
            {
                result = 0; 
            }

            //Assert
            NUnit.Framework.Assert.True(result == 0);
        }

        [Test]
        public void TryAddBadTicketNoDesc()
        {
            //In our Database - User, Subject, and Desc are all NOT NULL.
            //Attempting to add a record with null values here should fail.

            //Arrange
            _dbcontext = new SecureAppQaDbContext();
            AspNetUser user = _dbcontext.AspNetUsers.First();
            Ticket ticket = new Ticket()
            {
                AspNetUserId = user.Id,
                DateCreated = DateTime.UtcNow,
                Description = null,
                Subject = "Subject!",
                IsActive = true,
            };

            //Act
            int? result = null;
            try
            {
                _dbcontext.Tickets.Add(ticket);
                result = _dbcontext.SaveChanges();
            }
            catch
            {
                result = 0;
            }

            //Assert
            NUnit.Framework.Assert.True(result == 0);
        }

        [Test]
        public void TryAddBadTicketNoSubject()
        {
            //In our Database - User, Subject, and Desc are all NOT NULL.
            //Attempting to add a record with null values here should fail.

            //Arrange
            _dbcontext = new SecureAppQaDbContext();
            AspNetUser user = _dbcontext.AspNetUsers.First();
            Ticket ticket = new Ticket()
            {
                AspNetUserId = user.Id,
                DateCreated = DateTime.UtcNow,
                Description = "Description!",
                Subject = null,
                IsActive = true,
            };

            //Act
            int? result = null;
            try
            {
                _dbcontext.Tickets.Add(ticket);
                result = _dbcontext.SaveChanges();
            }
            catch
            {
                result = 0;
            }

            //Assert
            NUnit.Framework.Assert.True(result == 0);
        }
    }
}
