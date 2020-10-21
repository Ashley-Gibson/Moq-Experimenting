using Moq_Experimenting.Services.Models;

namespace Moq_Experimenting.Services
{
    public interface IPaymentService
    {
        bool Charge(double total, ICard card);
    }
}
