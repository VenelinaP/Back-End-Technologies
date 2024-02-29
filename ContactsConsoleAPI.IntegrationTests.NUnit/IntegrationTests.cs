using ContactsConsoleAPI.Business;
using ContactsConsoleAPI.Business.Contracts;
using ContactsConsoleAPI.Data.Models;
using ContactsConsoleAPI.DataAccess;
using ContactsConsoleAPI.DataAccess.Contrackts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsConsoleAPI.IntegrationTests.NUnit
{
    public class IntegrationTests
    {
        private TestContactDbContext dbContext;
        private IContactManager contactManager;

        [SetUp]
        public void SetUp()
        {
            this.dbContext = new TestContactDbContext();
            this.contactManager = new ContactManager(new ContactRepository(this.dbContext));
        }


        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.Dispose();
        }


        //positive test
        [Test]
        public async Task AddContactAsync_ShouldAddNewContact()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            var dbContact = await dbContext.Contacts.FirstOrDefaultAsync(c => c.Contact_ULID == newContact.Contact_ULID);

            Assert.NotNull(dbContact);
            Assert.AreEqual(newContact.FirstName, dbContact.FirstName);
            Assert.AreEqual(newContact.LastName, dbContact.LastName);
            Assert.AreEqual(newContact.Phone, dbContact.Phone);
            Assert.AreEqual(newContact.Email, dbContact.Email);
            Assert.AreEqual(newContact.Address, dbContact.Address);
            Assert.AreEqual(newContact.Contact_ULID, dbContact.Contact_ULID);
        }

        //Negative test
        [Test]
        public async Task AddContactAsync_TryToAddContactWithInvalidCredentials_ShouldThrowException()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "invalid_Mail", //invalid email
                Gender = "Male",
                Phone = "0889933779"
            };

            var ex = Assert.ThrowsAsync<ValidationException>(async () => await contactManager.AddAsync(newContact));
            var actual = await dbContext.Contacts.FirstOrDefaultAsync(c => c.Contact_ULID == newContact.Contact_ULID);

            Assert.IsNull(actual);
            Assert.That(ex?.Message, Is.EqualTo("Invalid contact!"));

        }

        [Test]
        public async Task DeleteContactAsync_WithValidULID_ShouldRemoveContactFromDb()
        {
            // Arrange
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            // Act
            await contactManager.DeleteAsync(newContact.Contact_ULID);

            // Assert
            var contactInDB = await dbContext.Contacts.FirstOrDefaultAsync(x => x.Contact_ULID == newContact.Contact_ULID);

            Assert.IsNull(contactInDB);
        }

        [Test]
        public async Task DeleteContactAsync_TryToDeleteWithNullOrWhiteSpaceULID_ShouldThrowException()
        {
            // Act and Assert
            Assert.ThrowsAsync<ArgumentException>(() => contactManager.DeleteAsync(null));
        }

        [Test]
        public async Task GetAllAsync_WhenContactsExist_ShouldReturnAllContacts()
        {
            // Arrange
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            var secondNewContact = new Contact()
            {
                FirstName = "SecondTestFirstName",
                LastName = "SecondTestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "2ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "secondtest@gmail.com",
                Gender = "Male",
                Phone = "0889933778"
            };

            await contactManager.AddAsync(secondNewContact);

            // Act
            var result = await contactManager.GetAllAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));

            var firstContact = result.First();
            Assert.That(firstContact.Email, Is.EqualTo(newContact.Email));
        }

        [Test]
        public async Task GetAllAsync_WhenNoContactsExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange

            // Act and Assert
            var expection = Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.GetAllAsync());

            Assert.That(expection.Message, Is.EqualTo("No contact found."));
        }

        [Test]
        public async Task SearchByFirstNameAsync_WithExistingFirstName_ShouldReturnMatchingContacts()
        {
            // Arrange
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            var secondNewContact = new Contact()
            {
                FirstName = "SecondTestFirstName",
                LastName = "SecondTestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "2ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "secondtest@gmail.com",
                Gender = "Male",
                Phone = "0889933778"
            };

            await contactManager.AddAsync(secondNewContact);

            // Act
            var result = await contactManager.SearchByFirstNameAsync(secondNewContact.FirstName);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            var itemInTheDb = result.First();
            Assert.That(itemInTheDb.LastName, Is.EqualTo(secondNewContact.LastName));

        }

        [Test]
        public async Task SearchByFirstNameAsync_WithNonExistingFirstName_ShouldThrowKeyNotFoundException()
        {

            // Act and Assert
            var exeption = Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.SearchByFirstNameAsync("NO_SUCH_KEY"));

            Assert.That(exeption.Message, Is.EqualTo("No contact found with the given first name."));

        }

        [Test]
        public async Task SearchByLastNameAsync_WithExistingLastName_ShouldReturnMatchingContacts()
        {
            // Arrange
            var newContact1 = new Contact()
            {
                FirstName = "TestFirstName1",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "test1@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            var newContact2 = new Contact()
            {
                FirstName = "TestFirstName2",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "2ABC23456HH",
                Email = "test2@gmail.com",
                Gender = "Male",
                Phone = "0889933778"
            };

            var newContacts = new List<Contact>() { newContact1, newContact2 };

            foreach (var contact in newContacts)
            {
                await contactManager.AddAsync(contact);
            }

            // Act
            var result = await contactManager.SearchByLastNameAsync(newContacts[0].LastName);

            // Assert
            Assert.That(result.Count, Is.EqualTo(newContacts.Count));
            foreach (var itemInDb in result)
            {
                Assert.That(itemInDb.LastName, Is.EqualTo(newContacts[0].LastName));
            }
        }

        [Test]
        public async Task SearchByLastNameAsync_WithNonExistingLastName_ShouldThrowKeyNotFoundException()
        {
            // Act and Assert
            var exeption = Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.SearchByLastNameAsync("NON_EXISTING_NAME"));

            Assert.That(exeption.Message, Is.EqualTo("No contact found with the given last name."));

        }
        [Test]
        public async Task GetSpecificAsync_WithValidULID_ShouldReturnContact()
        {
            // Arrange
            var newContact1 = new Contact()
            {
                FirstName = "TestFirstName1",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "test1@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            var newContact2 = new Contact()
            {
                FirstName = "TestFirstName2",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "2ABC23456HH",
                Email = "test2@gmail.com",
                Gender = "Male",
                Phone = "0889933778"
            };

            var newContacts = new List<Contact>() { newContact1, newContact2 };

            foreach (var contact in newContacts)
            {
                await contactManager.AddAsync(contact);
            }

            // Act
            var result = await contactManager.GetSpecificAsync(newContacts[1].Contact_ULID);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.FirstName, Is.EqualTo(newContacts[1].FirstName));
        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidULID_ShouldThrowKeyNotFoundException()
        {
            string invalidULID = "NON_VALID_ID";

            // Act and Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.GetSpecificAsync(invalidULID));

            Assert.That(exception.Message, Is.EqualTo($"No contact found with ULID: {invalidULID}"));

        }

        [Test]
        public async Task UpdateAsync_WithValidContact_ShouldUpdateContact()
        {
            // Arrange
            var newContact1 = new Contact()
            {
                FirstName = "TestFirstName1",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "test1@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            var newContact2 = new Contact()
            {
                FirstName = "TestFirstName2",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "2ABC23456HH",
                Email = "test2@gmail.com",
                Gender = "Male",
                Phone = "0889933778"
            };

            var newContacts = new List<Contact>() { newContact1, newContact2 };

            foreach (var contact in newContacts)
            {
                await contactManager.AddAsync(contact);
            }

            var modifiedContact = newContacts[0];
            modifiedContact.FirstName = "UPDATED!";

            // Act
            await contactManager.UpdateAsync(modifiedContact);

            // Assert
            var itemInDB = await dbContext.Contacts.FirstOrDefaultAsync(x => x.Contact_ULID == modifiedContact.Contact_ULID);

            Assert.NotNull(itemInDB);
            Assert.That(itemInDB.FirstName, Is.EqualTo(modifiedContact.FirstName));            
            
        }

        [Test]
        public async Task UpdateAsync_WithInvalidContact_ShouldThrowValidationException()
        {
           
            // Act and Assert
            var exeption = Assert.ThrowsAsync<ValidationException>(() => contactManager.UpdateAsync(new Contact ()));

            Assert.That(exeption.Message, Is.EqualTo("Invalid contact!"));
        }
    }
}




