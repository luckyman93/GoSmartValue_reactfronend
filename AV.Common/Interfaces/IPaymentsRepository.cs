using AV.Common.Entities;
using System;
using System.Threading.Tasks;

namespace AV.Common.Interfaces
{
    public interface IPaymentsRepository : IRepository<PaymentHistory>
    {
        Task<PaymentHistory> GetUnpaid(string token);
        Task AddPayment(PaymentHistory payment);
        Task<User> RenewAccountSubscription(Guid AccountId);
        Task SaveUpdatedPayment(PaymentHistory payment);
        Task<string> ActivateStandardUserSubscription(Guid accountId);
        Task<bool> HasTransactionRef(string paymentRef);
        Task UpdateAsync(PaymentHistory payment);
    }
}