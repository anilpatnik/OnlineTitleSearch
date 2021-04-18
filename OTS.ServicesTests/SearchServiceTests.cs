using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class SearchServiceTests
    {
        private Mock<IAppSettings> _mockAppSettings;
        private Mock<IUtilService> _mockUtilService;
        private Mock<ILookupRepository> _mockLookupRepository;
        private Mock<AbstractLogger<SearchService>> _mockLogger;
        private ISearchService _searchService;

        [TestInitialize]
        public void Initialize()
        {
            _mockAppSettings = new Mock<IAppSettings>();
            _mockUtilService = new Mock<IUtilService>();
            _mockLookupRepository = new Mock<ILookupRepository>();
            _mockLogger = new Mock<AbstractLogger<SearchService>>();
            _searchService = new SearchService(_mockAppSettings.Object, _mockUtilService.Object, _mockLookupRepository.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task GetAsyncTest_Multiple_Search_Engines()
        {
            // Arrange
            const string searchPage = "https://testing.com.au/{0}/page{1}.html";
            const int maxPages = 5;
            const string siteName = "Testing";
            var urls = new List<string>
            {
                "<cite>https://testing1001.com</cite>",
                "<cite>https://test1002.com</cite>",
                "<cite>https://email.com</cite>",
                "<cite>https://testing1004.com</cite>",
                "<cite>https://testing3.com</cite>",
                "<cite>https://facebook.com</cite>",
                "<cite>https://testing1002.com</cite>",
                "<cite>https://microsoft.com</cite>",
                "<cite>https://testing1003.com</cite>",
                "<cite>https://google.com</cite>"
            };
            var filteredUrls = new List<string> { "4","6","12" };

            _mockAppSettings.Setup(x => x.SearchPage).Returns(searchPage);
            _mockAppSettings.Setup(x => x.MaxPages).Returns(maxPages);
            _mockAppSettings.Setup(x => x.SiteName).Returns(siteName);
            _mockLookupRepository.Setup(x => x.ListAsync()).ReturnsAsync(MockDatabase.SearchEngines);
            _mockUtilService.Setup(x => x.GetUrls(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(urls);
            _mockUtilService.Setup(x => x.GetFilteredUrls(It.IsAny<IEnumerable<string>>(), It.IsAny<string>())).Returns(filteredUrls);

            // Act
            var response = await _searchService.GetAsync(It.IsAny<string>());

            // Assert
            _mockUtilService.Verify(x => x.GetUrls(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()), Times.Exactly(10));
            _mockUtilService.Verify(x => x.GetFilteredUrls(It.IsAny<IEnumerable<string>>(), It.IsAny<string>()), Times.Exactly(2));

            _mockLogger.Verify(x => x.Log(LogLevel.Information, It.IsAny<Exception>(), $"Loop through the search engine = Google"), Times.Once);
            _mockLogger.Verify(x => x.Log(LogLevel.Information, It.IsAny<Exception>(), $"Loop through the search engine = Bing"), Times.Once);
            _mockLogger.Verify(x => x.Log(LogLevel.Information, It.IsAny<Exception>(), $"Search Engine = Google and Page = 1"), Times.Once);
            _mockLogger.Verify(x => x.Log(LogLevel.Information, It.IsAny<Exception>(), $"Search Engine = Bing and Page = 3"), Times.Once);

            Assert.AreEqual(2, response.Count());
            IDictionary<string, object> firstPropertyValues = response.FirstOrDefault();
            IDictionary<string, object> lastPropertyValues = response.LastOrDefault();
            Assert.AreEqual("GOOGLE", firstPropertyValues.Values.First().ToString().ToUpper());
            Assert.AreEqual("BING", lastPropertyValues.Values.First().ToString().ToUpper());
            Assert.AreEqual("4, 6, 12", firstPropertyValues.Values.Last().ToString());
            Assert.AreEqual("4, 6, 12", lastPropertyValues.Values.Last().ToString());
        }

        [TestMethod]
        public async Task GetAsyncTest_Single_Search_Engines()
        {
            // Arrange
            const string searchPage = "https://testing.com.au/{0}/page{1}.html";
            const int maxPages = 5;
            const string siteName = "Testing";
            var searchEngines = new List<Lookup>() {new Lookup {Id = 1, Name = "Test"}};
            var urls = new List<string>
            {
                "<cite>https://testing1001.com</cite>",
                "<cite>https://test1002.com</cite>",
                "<cite>https://email.com</cite>",
                "<cite>https://testing1004.com</cite>",
                "<cite>https://testing3.com</cite>",
                "<cite>https://facebook.com</cite>",
                "<cite>https://testing1002.com</cite>",
                "<cite>https://microsoft.com</cite>",
                "<cite>https://testing1003.com</cite>",
                "<cite>https://google.com</cite>"
            };
            var filteredUrls = new List<string> { "4", "6", "12" };

            _mockAppSettings.Setup(x => x.SearchPage).Returns(searchPage);
            _mockAppSettings.Setup(x => x.MaxPages).Returns(maxPages);
            _mockAppSettings.Setup(x => x.SiteName).Returns(siteName);
            _mockLookupRepository.Setup(x => x.ListAsync()).ReturnsAsync(searchEngines);
            _mockUtilService.Setup(x => x.GetUrls(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(urls);
            _mockUtilService.Setup(x => x.GetFilteredUrls(It.IsAny<IEnumerable<string>>(), It.IsAny<string>())).Returns(filteredUrls);

            // Act
            var response = await _searchService.GetAsync(It.IsAny<string>());

            // Assert
            _mockUtilService.Verify(x => x.GetUrls(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()), Times.Exactly(5));
            _mockUtilService.Verify(x => x.GetFilteredUrls(It.IsAny<IEnumerable<string>>(), It.IsAny<string>()), Times.Exactly(1));

            _mockLogger.Verify(x => x.Log(LogLevel.Information, It.IsAny<Exception>(), $"Loop through the search engine = Test"), Times.Once);
            _mockLogger.Verify(x => x.Log(LogLevel.Information, It.IsAny<Exception>(), $"Search Engine = Test and Page = 5"), Times.Once);

            Assert.AreEqual(1, response.Count());
            IDictionary<string, object> firstPropertyValues = response.FirstOrDefault();
            Assert.AreEqual("TEST", firstPropertyValues.Values.First().ToString().ToUpper());
            Assert.AreEqual("4, 6, 12", firstPropertyValues.Values.Last().ToString());
        }

        [TestMethod]
        public async Task GetAsyncTest_No_Search_Engines()
        {
            // Arrange
            const string searchPage = "https://testing.com.au/{0}/page{1}.html";
            const int maxPages = 5;
            const string siteName = "Testing";
            var searchEngines = new List<Lookup>();
            var urls = new List<string>
            {
                "<cite>https://facebook.com</cite>",
                "<cite>https://testing1002.com</cite>",
                "<cite>https://microsoft.com</cite>",
                "<cite>https://testing1003.com</cite>",
                "<cite>https://google.com</cite>"
            };
            var filteredUrls = new List<string> { "4", "6", "12" };

            _mockAppSettings.Setup(x => x.SearchPage).Returns(searchPage);
            _mockAppSettings.Setup(x => x.MaxPages).Returns(maxPages);
            _mockAppSettings.Setup(x => x.SiteName).Returns(siteName);
            _mockLookupRepository.Setup(x => x.ListAsync()).ReturnsAsync(searchEngines);
            _mockUtilService.Setup(x => x.GetUrls(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(urls);
            _mockUtilService.Setup(x => x.GetFilteredUrls(It.IsAny<IEnumerable<string>>(), It.IsAny<string>())).Returns(filteredUrls);

            // Act
            var response = await _searchService.GetAsync(It.IsAny<string>());

            // Assert
            _mockUtilService.Verify(x => x.GetUrls(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()), Times.Never);
            _mockUtilService.Verify(x => x.GetFilteredUrls(It.IsAny<IEnumerable<string>>(), It.IsAny<string>()), Times.Never);

            Assert.AreEqual(0, response.Count());
        }

        [TestMethod]
        public async Task GetAsyncTest_Exception()
        {
            // Arrange
            const string searchPage = "https://testing.com.au/{0}/page{1}.html";
            const int maxPages = 5;
            const string siteName = "Testing";
            var urls = new List<string>
            {
                "<cite>https://facebook.com</cite>",
                "<cite>https://testing1002.com</cite>",
                "<cite>https://microsoft.com</cite>",
                "<cite>https://testing1003.com</cite>",
                "<cite>https://google.com</cite>"
            };
            var filteredUrls = new List<string> { "4", "6", "12" };

            _mockAppSettings.Setup(x => x.SearchPage).Returns(searchPage);
            _mockAppSettings.Setup(x => x.MaxPages).Returns(maxPages);
            _mockAppSettings.Setup(x => x.SiteName).Returns(siteName);
            _mockLookupRepository.Setup(x => x.ListAsync()).ReturnsAsync(MockDatabase.SearchEngines);
            _mockUtilService.Setup(x => x.GetUrls(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Throws(new Exception("Custom Error"));

            // Act
            var response = await _searchService.GetAsync(It.IsAny<string>());

            // Assert
            _mockUtilService.Verify(x => x.GetUrls(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()), Times.Exactly(2));
            _mockUtilService.Verify(x => x.GetFilteredUrls(It.IsAny<IEnumerable<string>>(), It.IsAny<string>()), Times.Never);
            _mockLogger.Verify(x => x.Log(LogLevel.Error, It.IsAny<Exception>(), $"{nameof(SearchService)} GetAsync Custom Error"), Times.Exactly(2));

            Assert.AreEqual(0, response.Count());
        }
    }
}