using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OTS.Models;
using OTS.Repositories.Contexts;
using OTS.Repositories.Interfaces;
using OTS.Services;
using OTS.Services.Interfaces;

namespace OTS.ServicesTests
{
    [TestClass]
    public class LookupServiceTests
    {
        private Mock<ILookupRepository> _mockLookupRepository;
        private Mock<ISaveRepository> _mockSaveRepository;
        private Mock<IMemoryCache> _mockCache;
        private Mock<AbstractLogger<LookupService>> _mockLogger;
        private ILookupService _lookupService;

        [TestInitialize]
        public void Initialize()
        {
            _mockLookupRepository = new Mock<ILookupRepository>();
            _mockSaveRepository = new Mock<ISaveRepository>();
            _mockCache = new Mock<IMemoryCache>();
            _mockLogger = new Mock<AbstractLogger<LookupService>>();
            _lookupService = new LookupService(_mockLookupRepository.Object, _mockSaveRepository.Object, _mockCache.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task ListAsyncTest()
        {
            // Arrange
            _mockCache.Setup(m => m.CreateEntry(It.IsAny<object>())).Returns(Mock.Of<ICacheEntry>());
            _mockLookupRepository.Setup(x => x.ListAsync()).ReturnsAsync(MockDatabase.SearchEngines);

            // Act
            var response = await _lookupService.ListAsync();

            // Assert
            Assert.AreEqual(2, response.Count());
            Assert.AreEqual("GOOGLE", response.FirstOrDefault().Name.ToUpper());
            Assert.AreEqual("BING", response.LastOrDefault().Name.ToUpper());
        }

        [TestMethod]
        public async Task FindByIdAsyncTest()
        {
            // Arrange
            var lookup = new Lookup { Id = 3, Name = "Twitter" };
            _mockLookupRepository.Setup(x => x.FindByIdAsync(3)).ReturnsAsync(lookup);

            // Act
            var response = await _lookupService.FindByIdAsync(3);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.AreEqual("", response.Message);
            Assert.AreEqual(3, response.Resource.Id);
            Assert.AreEqual("TWITTER", response.Resource.Name.ToUpper());
        }

        [TestMethod]
        public async Task FindByIdAsyncTest_NotFound()
        {
            // Arrange
            var lookup = new Lookup { Id = 3, Name = "Twitter" };
            _mockLookupRepository.Setup(x => x.FindByIdAsync(3)).ReturnsAsync(lookup);

            // Act
            var response = await _lookupService.FindByIdAsync(2);

            // Assert
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Search engine not found.", response.Message);
        }

        [TestMethod]
        public async Task SaveAsyncTest()
        {
            // Act
            var response = await _lookupService.SaveAsync(It.IsAny<Lookup>());

            // Assert
            _mockLookupRepository.Verify(x => x.Add(It.IsAny<Lookup>()), Times.Once);
            _mockSaveRepository.Verify(x => x.CompleteAsync(), Times.Once);

            Assert.IsTrue(response.Success);
            Assert.AreEqual("", response.Message);
        }

        [TestMethod]
        public async Task SaveAsyncTest_Exception()
        {
            // Arrange
            _mockLookupRepository.Setup(x => x.Add(It.IsAny<Lookup>())).Throws(new Exception("Custom Error"));

            // Act
            var response = await _lookupService.SaveAsync(It.IsAny<Lookup>());

            // Assert
            _mockLookupRepository.Verify(x => x.Add(It.IsAny<Lookup>()), Times.Once);
            _mockSaveRepository.Verify(x => x.CompleteAsync(), Times.Never);
            _mockLogger.Verify(x => x.Log(LogLevel.Error, It.IsAny<Exception>(), $"{nameof(LookupService)} SaveAsync Custom Error"), Times.Once);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Custom Error", response.Message);
        }

        [TestMethod]
        public async Task UpdateAsyncTest()
        {
            // Arrange
            var lookup = new Lookup { Id = 1, Name = "Google" };
            _mockLookupRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(lookup);

            // Act
            var response = await _lookupService.UpdateAsync(It.IsAny<int>(), lookup);

            // Assert
            _mockLookupRepository.Verify(x => x.Update(lookup), Times.Once);
            _mockSaveRepository.Verify(x => x.CompleteAsync(), Times.Once);

            Assert.IsTrue(response.Success);
            Assert.AreEqual("", response.Message);
        }

        [TestMethod]
        public async Task UpdateAsyncTest_NotFound()
        {
            // Arrange
            _mockLookupRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync((Lookup) null);

            // Act
            var response = await _lookupService.UpdateAsync(It.IsAny<int>(), It.IsAny<Lookup>());

            // Assert
            _mockLookupRepository.Verify(x => x.Update(It.IsAny<Lookup>()), Times.Never);
            _mockSaveRepository.Verify(x => x.CompleteAsync(), Times.Never);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Search engine not found.", response.Message);
        }

        [TestMethod]
        public async Task UpdateAsyncTest_Exception()
        {
            // Arrange
            var lookup = new Lookup { Id = 1, Name = "Google" };
            _mockLookupRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(lookup);
            _mockLookupRepository.Setup(x => x.Update(lookup)).Throws(new Exception("Custom Error"));

            // Act
            var response = await _lookupService.UpdateAsync(It.IsAny<int>(), lookup);

            // Assert
            _mockLookupRepository.Verify(x => x.Update(lookup), Times.Once);
            _mockSaveRepository.Verify(x => x.CompleteAsync(), Times.Never);
            _mockLogger.Verify(x => x.Log(LogLevel.Error, It.IsAny<Exception>(), $"{nameof(LookupService)} UpdateAsync Custom Error"), Times.Once);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Custom Error", response.Message);
        }

        [TestMethod]
        public async Task DeleteAsyncTest()
        {
            // Arrange
            var lookup = new Lookup { Id = 1, Name = "Google" };
            _mockLookupRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(lookup);

            // Act
            var response = await _lookupService.DeleteAsync(It.IsAny<int>());

            // Assert
            _mockLookupRepository.Verify(x => x.Remove(It.IsAny<Lookup>()), Times.Once);
            _mockSaveRepository.Verify(x => x.CompleteAsync(), Times.Once);

            Assert.IsTrue(response.Success);
            Assert.AreEqual("", response.Message);
        }

        [TestMethod]
        public async Task DeleteAsyncTest_NotFound()
        {
            // Arrange
            _mockLookupRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync((Lookup)null);

            // Act
            var response = await _lookupService.DeleteAsync(It.IsAny<int>());

            // Assert
            _mockLookupRepository.Verify(x => x.Remove(It.IsAny<Lookup>()), Times.Never);
            _mockSaveRepository.Verify(x => x.CompleteAsync(), Times.Never);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Search engine not found.", response.Message);
        }

        [TestMethod]
        public async Task DeleteAsyncTest_Exception()
        {
            // Arrange
            var lookup = new Lookup { Id = 1, Name = "Google" };
            _mockLookupRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(lookup);
            _mockLookupRepository.Setup(x => x.Remove(It.IsAny<Lookup>())).Throws(new Exception("Custom Error"));

            // Act
            var response = await _lookupService.DeleteAsync(It.IsAny<int>());

            // Assert
            _mockLookupRepository.Verify(x => x.Remove(It.IsAny<Lookup>()), Times.Once);
            _mockSaveRepository.Verify(x => x.CompleteAsync(), Times.Never);
            _mockLogger.Verify(x => x.Log(LogLevel.Error, It.IsAny<Exception>(), $"{nameof(LookupService)} DeleteAsync Custom Error"), Times.Once);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Custom Error", response.Message);
        }
    }
}