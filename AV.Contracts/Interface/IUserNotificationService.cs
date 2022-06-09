using System.Threading.Tasks;
using AV.Contracts.Models.Users;

namespace AV.Contracts.Interface
{
    public interface IUserNotificationService
    {
        Task SendConfirmationEmail(UserModel user);
    }
}
