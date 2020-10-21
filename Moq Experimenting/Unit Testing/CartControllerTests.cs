using Moq_Experimenting.Services;
using Moq_Experimenting.Services.Models;
using Moq;
using NUnit.Framework;
using Moq_Experimenting.Controllers;
using System.Linq;
using System.Collections.Generic;

namespace test
{
    public class Tests
    {
        private CartController controller;
        private Mock<IPaymentService> paymentServiceMock;
        private Mock<ICartService> cartServiceMock;

        private Mock<IShipmentService> shipmentServiceMock;
        private Mock<ICard> cardMock;
        private Mock<IAddressInfo> addressInfoMock;
        private List<ICartItem> items;

        [SetUp]
        public void Setup()
        {

            cartServiceMock = new Mock<ICartService>();
            paymentServiceMock = new Mock<IPaymentService>();
            shipmentServiceMock = new Mock<IShipmentService>();

            // arrange
            cardMock = new Mock<ICard>();
            addressInfoMock = new Mock<IAddressInfo>();

            // 
            var cartItemMock = new Mock<ICartItem>();
            cartItemMock.Setup(item => item.Price).Returns(10);

            items = new List<ICartItem>()
            {
                cartItemMock.Object
            };

            cartServiceMock.Setup(c => c.Items()).Returns(items.AsEnumerable());

            controller = new CartController(cartServiceMock.Object, paymentServiceMock.Object, shipmentServiceMock.Object);
        }

        [Test]
        public void ShouldReturnCharged()
        {
            paymentServiceMock.Setup(p => p.Charge(It.IsAny<double>(), cardMock.Object)).Returns(true);

            // act
            var result = controller.CheckOut(cardMock.Object, addressInfoMock.Object);

            // assert
            // myInterfaceMock.Verify((m => m.DoesSomething()), Times.Once());
            shipmentServiceMock.Verify(s => s.Ship(addressInfoMock.Object, items.AsEnumerable()), Times.Once());

            Assert.AreEqual("charged", result);
        }

        [Test]
        public void ShouldReturnNotCharged()
        {
            paymentServiceMock.Setup(p => p.Charge(It.IsAny<double>(), cardMock.Object)).Returns(false);

            // act
            var result = controller.CheckOut(cardMock.Object, addressInfoMock.Object);

            // assert
            shipmentServiceMock.Verify(s => s.Ship(addressInfoMock.Object, items.AsEnumerable()), Times.Never());
            Assert.AreEqual("not charged", result);
        }
    }
}