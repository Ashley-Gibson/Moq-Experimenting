using Moq_Experimenting.Services.Models;
using System.Collections.Generic;

namespace Moq_Experimenting.Services
{
    public interface IShipmentService
    {
        void Ship(IAddressInfo info, IEnumerable<ICartItem> items);
    }
}
