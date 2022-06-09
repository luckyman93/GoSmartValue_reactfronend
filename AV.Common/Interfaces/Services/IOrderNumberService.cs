using System.Threading.Tasks;

namespace AV.Common.Interfaces.Services
{
    public interface IOrderNumberService
    {
        Task<string> GenerateNextOrderNo();
    }
}
