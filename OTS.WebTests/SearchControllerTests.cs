using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OTS.Services.Interfaces;
using OTS.Web.Controllers;

namespace OTS.WebTests
{
    [TestClass]
    public class SearchControllerTests
    {
        private Mock<ISearchService> _mockSearchService;
        private SearchController _searchController;

        [TestInitialize]
        public void Initialize()
        {
            _mockSearchService = new Mock<ISearchService>();
            _searchController = new SearchController(_mockSearchService.Object);
        }

        [TestMethod]
        public async Task GetAsyncTest()
        {
            // Arrange
            dynamic mockData1 = new ExpandoObject(); mockData1.SearchEngine = "Google"; mockData1.RankingOrder = "1, 2, 3, 5";
            dynamic mockData2 = new ExpandoObject(); mockData2.SearchEngine = "Bing"; mockData2.RankingOrder = "1, 5, 6, 7";
            dynamic mockData3 = new ExpandoObject(); mockData3.SearchEngine = "Facebook"; mockData3.RankingOrder = "2, 6, 7, 8";
            var results = new List<dynamic> { mockData1, mockData2, mockData3 };
            _mockSearchService.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(results);
            
            // Act
            var response = await _searchController.GetAsync(It.IsAny<string>());

            // Assert
            Assert.AreEqual(3, response.Count());
            IDictionary<string, object> firstPropertyValues = response.FirstOrDefault();
            IDictionary<string, object> lastPropertyValues = response.LastOrDefault();
            Assert.AreEqual("GOOGLE", firstPropertyValues.Values.First().ToString().ToUpper());
            Assert.AreEqual("FACEBOOK", lastPropertyValues.Values.First().ToString().ToUpper());
            Assert.AreEqual("1, 2, 3, 5", firstPropertyValues.Values.Last().ToString());
            Assert.AreEqual("2, 6, 7, 8", lastPropertyValues.Values.Last().ToString());
        }
    }
}