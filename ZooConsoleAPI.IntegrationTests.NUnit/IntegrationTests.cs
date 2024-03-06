using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using ZooConsoleAPI.Business;
using ZooConsoleAPI.Business.Contracts;
using ZooConsoleAPI.Data.Model;
using ZooConsoleAPI.DataAccess;

namespace ZooConsoleAPI.IntegrationTests.NUnit
{
    public class IntegrationTests
    {
        private TestAnimalDbContext dbContext;
        private IAnimalsManager animalsManager;

        [SetUp]
        public void SetUp()
        {
            this.dbContext = new TestAnimalDbContext();
            this.animalsManager = new AnimalsManager(new AnimalRepository(this.dbContext));
        }


        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.Dispose();
        }


        //positive test
        [Test]
        public async Task AddAnimalAsync_ShouldAddNewAnimal()
        {
            // Arrange
            var newAnimal = new Animal()
            {
                CatalogNumber = "123456789012",
                Name = "Name",
                Breed = "Some animal",
                Type = "Type",
                Age = 5,
                Gender = "Male",
                IsHealthy = true
            };

            // Act
            await animalsManager.AddAsync(newAnimal);

            // Assert
            var dbAnimal = await dbContext.Animals.FirstOrDefaultAsync(a => a.CatalogNumber == newAnimal.CatalogNumber);
            Assert.NotNull(dbAnimal);
            Assert.AreEqual(newAnimal.Name, dbAnimal.Name);
            Assert.AreEqual(newAnimal.Breed, dbAnimal.Breed);
            Assert.AreEqual(newAnimal.Type, dbAnimal.Type);
            Assert.AreEqual(newAnimal.Age, dbAnimal.Age);
            Assert.AreEqual(newAnimal.Gender, dbAnimal.Gender);
            Assert.AreEqual(newAnimal.IsHealthy, dbAnimal.IsHealthy);
        }

        //Negative test
        [Test]
        public async Task AddAnimalAsync_TryToAddAnimalWithInvalidCredentials_ShouldThrowException()
        {
            // Arrange
            var newAnimal = new Animal()
            {
                CatalogNumber = "1234567",
                Name = "Name",
                Breed = "Some animal",
                Type = "Type",
                Age = 5,
                Gender = "Male",
                IsHealthy = true
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<ValidationException>(async () => await animalsManager.AddAsync(newAnimal));
            var actual = await dbContext.Animals.FirstOrDefaultAsync(c => c.CatalogNumber == newAnimal.CatalogNumber);

            Assert.IsNull(actual);
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message, Is.EqualTo("Invalid animal!"));
        }
    
        [Test]
        public async Task DeleteAnimalAsync_WithValidCatalogNumber_ShouldRemoveAnimalFromDb()
        {
            //Arrange
            var newAnimal = new Animal()
            {
                CatalogNumber = "123456789012",
                Name = "Name",
                Breed = "Some animal",
                Type = "Type",
                Age = 5,
                Gender = "Male",
                IsHealthy = true
            };

            await animalsManager.AddAsync(newAnimal);

            // Act
            await animalsManager.DeleteAsync(newAnimal.CatalogNumber);

            // Assert
            var animalInDB = await dbContext.Animals.FirstOrDefaultAsync(x => x.CatalogNumber == newAnimal.CatalogNumber);

            Assert.IsNull(animalInDB);
        }

        [Test]
        public async Task DeleteAnimalAsync_TryToDeleteWithNullOrWhiteSpaceCatalogNumber_ShouldThrowException()
        {
            // Act and Assert
            Assert.ThrowsAsync<ArgumentException>(() => animalsManager.DeleteAsync(null));
        }

        [Test]
        public async Task GetAllAsync_WhenAnimalsExist_ShouldReturnAllAnimals()
        {
            // Arrange
            var newAnimal = new Animal()
            {
                CatalogNumber = "123456789012",
                Name = "Name",
                Breed = "Some animal",
                Type = "Type",
                Age = 5,
                Gender = "Male",
                IsHealthy = true
            };

            await animalsManager.AddAsync(newAnimal);

            var secondNewAnimal = new Animal()
            {
                CatalogNumber = "123456789013",
                Name = "Name",
                Breed = "Some animal",
                Type = "Type",
                Age = 4,
                Gender = "Female",
                IsHealthy = true
            };
            await animalsManager.AddAsync(secondNewAnimal);

            // Act
            var result = await animalsManager.GetAllAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));

            var firstAnimal = result.First();
            Assert.That(firstAnimal.Gender, Is.EqualTo(newAnimal.Gender));
            
        }

        [Test]
        public async Task GetAllAsync_WhenNoAnimalsExist_ShouldThrowKeyNotFoundException()
        {
            // Act and Assert
            var expection = Assert.ThrowsAsync<KeyNotFoundException>(() => animalsManager.GetAllAsync());

            Assert.That(expection.Message, Is.EqualTo("No animal found."));
        }

        [Test]
        public async Task SearchByTypeAsync_WithExistingType_ShouldReturnMatchingAnimals()
        {
            // Arrange
            
            var secondNewAnimal = new Animal()
            {
                CatalogNumber = "123456789013",
                Name = "Name",
                Breed = "Some animal",
                Type = "Type",
                Age = 4,
                Gender = "Female",
                IsHealthy = true
            };
            await animalsManager.AddAsync(secondNewAnimal);

            // Act
            var result = await animalsManager.SearchByTypeAsync(secondNewAnimal.Type);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            var itemInTheDb = result.First();
            Assert.That(itemInTheDb.Type, Is.EqualTo(secondNewAnimal.Type));

        }
    
        [Test]
        public async Task SearchByTypeAsync_WithNonExistingType_ShouldThrowKeyNotFoundException()
        {
            // Act and Assert
            var exeption = Assert.ThrowsAsync<KeyNotFoundException>(() => animalsManager.SearchByTypeAsync("NO_SUCH_KEY"));

            Assert.That(exeption.Message, Is.EqualTo("No animal found with the given type."));
        }

        [Test]
        public async Task GetSpecificAsync_WithValidCatalogNumber_ShouldReturnAnimal()
        {
            // Arrange
            var newAnimal = new Animal()
            {
                CatalogNumber = "123456789012",
                Name = "Name",
                Breed = "Some animal",
                Type = "Type",
                Age = 5,
                Gender = "Male",
                IsHealthy = true
            };

            await animalsManager.AddAsync(newAnimal);

            var secondNewAnimal = new Animal()
            {
                CatalogNumber = "123456789013",
                Name = "Name",
                Breed = "Some animal",
                Type = "Type",
                Age = 4,
                Gender = "Female",
                IsHealthy = true
            };
            
            // Act
            await animalsManager.AddAsync(secondNewAnimal);

            // Act
            var result = await animalsManager.GetSpecificAsync(secondNewAnimal.CatalogNumber);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.CatalogNumber, Is.EqualTo(secondNewAnimal.CatalogNumber));
        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidCatalogNumber_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            string invalidCatalogNumber = "NON_VALID_NUMBER";

            // Act and Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => animalsManager.GetSpecificAsync(invalidCatalogNumber));

            Assert.That(exception.Message, Is.EqualTo($"No animal found with catalog number: {invalidCatalogNumber}"));
        }

        [Test]
        public async Task UpdateAsync_WithValidAnimal_ShouldUpdateAnimal()
        {
            var newAnimal = new Animal()
            {
                CatalogNumber = "123456789012",
                Name = "Name",
                Breed = "Some animal",
                Type = "Type",
                Age = 5,
                Gender = "Male",
                IsHealthy = true
            };

            await animalsManager.AddAsync(newAnimal);

            var secondNewAnimal = new Animal()
            {
                CatalogNumber = "123456789013",
                Name = "Name",
                Breed = "Some animal",
                Type = "Type",
                Age = 4,
                Gender = "Female",
                IsHealthy = true
            };
            var newAnimals = new List<Animal>() { secondNewAnimal };

            foreach (var animal in newAnimals)
            {
                await animalsManager.AddAsync(animal);
            }

            var modifiedAnimal = newAnimals[0];
            modifiedAnimal.Name = "UPDATED!";

            // Act
            await animalsManager.UpdateAsync(modifiedAnimal);

            // Assert
            var itemInDB = await dbContext.Animals.FirstOrDefaultAsync(x => x.CatalogNumber == modifiedAnimal.CatalogNumber);

            Assert.NotNull(itemInDB);
            Assert.That(itemInDB.Name, Is.EqualTo(modifiedAnimal.Name));

        }

        [Test]
        public async Task UpdateAsync_WithInvalidAnimal_ShouldThrowValidationException()
        {
            // Act and Assert
            var exeption = Assert.ThrowsAsync<ValidationException>(() => animalsManager.UpdateAsync(new Animal()));

            Assert.That(exeption.Message, Is.EqualTo("Invalid animal!"));
        }
    }
}

