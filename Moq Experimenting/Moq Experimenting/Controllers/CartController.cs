using Microsoft.AspNetCore.Mvc;
using Moq_Experimenting.Services;
using Moq_Experimenting.Services.Models;

namespace Moq_Experimenting.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController
    {
        private readonly ICartService _cartService;
        private readonly IPaymentService _paymentService;
        private readonly IShipmentService _shipmentService;

        public CartController(
          ICartService cartService,
          IPaymentService paymentService,
          IShipmentService shipmentService
        )
        {
            _cartService = cartService;
            _paymentService = paymentService;
            _shipmentService = shipmentService;
        }

        [HttpPost]
        public string CheckOut(ICard card, IAddressInfo addressInfo)
        {
            var result = _paymentService.Charge(_cartService.Total(), card);
            if (result)
            {
                _shipmentService.Ship(addressInfo, _cartService.Items());
                return "charged";
            }
            else
            {
                return "not charged";
            }
        }
    }
}