using Chatroom.Bot;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Chatroom.UnitTest
{
    [TestClass]
    public class BotServiceTest
    {
        private IStockService stockServ;
        private IBotService service;

        [TestInitialize]
        public void Initialize()
        {
            this.stockServ = Mock.Of<IStockService>();

            this.service = new BotService(this.stockServ);
        }

        [DataTestMethod]
        [DataRow("   ")]
        [DataRow("hello")]
        [DataRow("/stock=")]
        [TestCategory("IsValidCommand")]
        public void IsValid_WithInvalidEntryCommand_ShouldReturnFalse(string text)
        {
            //Arrange - Act
            var result = this.service.IsValidCommand(text);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("IsValidCommand")]
        public void IsValid_WithInvalidEntryCommand_ShouldReturnTrue()
        {
            //Arrange - Act
            var result = this.service.IsValidCommand("/stock=test");

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("ProcessCommand")]
        public void ProcessCommand_ShouldReturnInvalidStock()
        {
            //Act 
            Stock stock = null;
            Mock.Get(this.stockServ).Setup(s => s.GetStock("stock_code")).ReturnsAsync(stock);
            
            //Act
            var result = this.service.ProcessCommand("/stock=stock_code");

            //Assert
            Assert.IsTrue(result.Result.ToLower().Contains("invalid"));
        }

        [TestMethod]
        [TestCategory("ProcessCommand")]
        public void ProcessCommand_ShouldReturnStockPrice()
        {
            //Act 
            Stock stock = new Stock() { Ticker = "aapl.us", Price= "150"};
            Mock.Get(this.stockServ).Setup(s => s.GetStock("aapl.us")).ReturnsAsync(stock);

            //Act
            var result = this.service.ProcessCommand("/stock=aapl.us");

            //Assert
            Assert.IsTrue(result.Result.Contains("quote"));
            Assert.IsTrue(result.Result.Contains("share"));
        }
    }
}
