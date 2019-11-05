using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chatroom.Service;
using Chatroom.WebApi;
using Moq;
using System.Collections.Generic;
using Chatroom.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Chatroom.UnitTest
{
    [TestClass]
    public class MessagesControllerTest
    {
        private MessagesController controller;
        private IMessagesServ messagesServ;
        private IHubContext<ChatHub> hubContext;

        [TestInitialize]
        public void Initialize()
        {
            this.messagesServ = Mock.Of<IMessagesServ>();
            this.hubContext = Mock.Of<IHubContext<ChatHub>>();

            this.controller = new MessagesController(this.hubContext, this.messagesServ);
        }

        [TestMethod]
        [TestCategory("GetMessages")]
        public void GetMessages_ShouldReturnNoMessages()
        {
            //Arrange
            IEnumerable<Message> messages = null;
            Mock.Get(this.messagesServ).Setup(s => s.GetMessages()).ReturnsAsync(messages);

            //Act
            var result = this.controller.GetMessages();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var objectResult = result.Result as OkObjectResult;
            Assert.IsNull(objectResult.Value);
        }

        [TestMethod]
        [TestCategory("GetMessages")]
        public void GetMessages_ShouldReturn3Messages()
        {
            //Arrange
            var messages = Enumerable.Repeat(new Message(), 3);
            Mock.Get(this.messagesServ).Setup(s => s.GetMessages()).ReturnsAsync(messages);

            //Act
            var result = this.controller.GetMessages();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var objectResult = result.Result as OkObjectResult;
            var content = objectResult.Value as IEnumerable<Message>;
            Assert.IsTrue(content.Count() == 3);
        }

        [TestMethod]
        [TestCategory("SendMessage")]
        public void SendMessage_ShouldCallService()
        {
            //Arrange
            var message = new Message() { Name = "Prueba", Text = "Hello" };
            Mock.Get(this.messagesServ).Setup(s => s.IsMessageForBot(message.Text)).Returns(false);

            //Act
            var result = this.controller.SendMessage(message);

            //Assert
            Mock.Get(this.messagesServ).Verify(s => s.SaveMessage(message), Times.Once);
            //Mock.Get(this.hubContext).Verify(h => h.Clients.All.SendAsync("shareMessage", message), Times.Once);
        }

        [TestMethod]
        [TestCategory("SendMessage")]
        public void SendMessage_ShouldNotCallService()
        {
            //Arrange
            var message = new Message() { Name = "Prueba", Text = "/stock=" };
            Mock.Get(this.messagesServ).Setup(s => s.IsMessageForBot(message.Text)).Returns(true);

            //Act
            var result = this.controller.SendMessage(message);

            //Assert
            Mock.Get(this.messagesServ).Verify(s => s.SaveMessage(message), Times.Never);
            //Mock.Get(this.hubContext).Verify(h => h.Clients.All.SendAsync("shareMessage", message), Times.Once);
        }
    }
}
