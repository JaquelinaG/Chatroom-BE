using Chatroom.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chatroom.UnitTest
{
    [TestClass]
    public class MessagesServiceTest
    {
        private IMessagesServ serv;

        [TestInitialize]
        public void Initialize()
        {
            this.serv = new MessagesServ();
        }

        [TestMethod]
        [TestCategory("IsMessageForBot")]
        public void IsMessageForBot_ShouldReturnFalse()
        {
            //Arrange-Act
            var result = this.serv.IsMessageForBot("test");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("IsMessageForBot")]
        public void IsMessageForBot_ShouldReturnTrue()
        {
            //Arrange-Act
            var result = this.serv.IsMessageForBot("/stock=");

            //Assert
            Assert.IsTrue(result);
        }
    }
}
