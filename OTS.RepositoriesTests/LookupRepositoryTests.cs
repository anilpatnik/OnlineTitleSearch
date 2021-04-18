using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OTS.Models;
using OTS.Repositories;
using OTS.Repositories.Contexts;
using OTS.Repositories.Interfaces;

namespace OTS.RepositoriesTests
{
    [TestClass]
    public class LookupRepositoryTests
    {
        private DbContextOptionsBuilder _builder;
        private AppDbContext _appDbContext;
        private ILookupRepository _lookupRepository;
        private ISaveRepository _saveRepository;

        [TestInitialize]
        public void Initialize()
        {
            _builder = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            _appDbContext = new AppDbContext(_builder.Options);
            _lookupRepository = new LookupRepository(_appDbContext);
            _saveRepository = new SaveRepository(_appDbContext);
        }

        [TestMethod]
        public async Task ListAsyncTest()
        {
            // Arrange
            _appDbContext.Lookups.AddRange(MockDatabase.SearchEngines);

            // Act
            var response = await _lookupRepository.ListAsync();

            // Assert
            Assert.AreEqual(2, response.Count());
            Assert.AreEqual("GOOGLE", response.FirstOrDefault().Name.ToUpper());
            Assert.AreEqual("BING", response.LastOrDefault().Name.ToUpper());
        }

        [TestMethod]
        public async Task FindByIdAsyncTest()
        {
            // Arrange
            _appDbContext.Lookups.AddRange(MockDatabase.SearchEngines);

            // Act
            var response = await _lookupRepository.FindByIdAsync(1);

            // Assert
            Assert.AreEqual(1, response.Id);
            Assert.AreEqual("GOOGLE", response.Name.ToUpper());
        }

        [TestMethod]
        public async Task AddTest()
        {
            // Arrange
            var lookup = new Lookup { Name = "Facebook" };

            // Act
            _lookupRepository.Add(lookup);
            await _saveRepository.CompleteAsync();

            var response = await _lookupRepository.ListAsync();
            var searchEngine = await _lookupRepository.FindByIdAsync(3);

            // Assert
            Assert.AreEqual(3, response.Count());
            Assert.AreEqual("FACEBOOK", searchEngine.Name.ToUpper());
        }

        [TestMethod]
        public async Task UpdateTest()
        {
            // Arrange
            var lookup = new Lookup { Name = "Facebook" };

            // Act
            _lookupRepository.Add(lookup);
            await _saveRepository.CompleteAsync();

            lookup.Name = "Twitter";
            _lookupRepository.Update(lookup);
            await _saveRepository.CompleteAsync();

            var response = await _lookupRepository.ListAsync();
            var searchEngine = await _lookupRepository.FindByIdAsync(3);

            // Assert
            Assert.AreEqual(3, response.Count());
            Assert.AreEqual("TWITTER", searchEngine.Name.ToUpper());
        }

        [TestMethod]
        public async Task RemoveTest()
        {
            // Arrange
            var lookup = new Lookup { Name = "Facebook" };

            // Act
            _lookupRepository.Add(lookup);
            await _saveRepository.CompleteAsync();

            _lookupRepository.Remove(lookup);
            await _saveRepository.CompleteAsync();

            var response = await _lookupRepository.ListAsync();
            
            // Assert
            Assert.AreEqual(2, response.Count());
        }
    }
}