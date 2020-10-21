using System.Collections.Generic;
using Moq_Experimenting.Services.Models;

namespace Moq_Experimenting.Services
{
    public interface ICartService
    {
        double Total();
        IEnumerable<ICartItem> Items();
    }
}