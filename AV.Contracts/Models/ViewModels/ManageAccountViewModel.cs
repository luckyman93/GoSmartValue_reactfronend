using System.Collections.Generic;

namespace AV.Contracts.Models.ViewModels
{
    public class ManageAccountViewModel
    {
        public ManageAccountViewModel()
        {
            BankAccounts = new List<BankAccount>();
        }
        public UserModel User { get; set; }
        public Account Account { get; set; }
        public IEnumerable<BankAccount> BankAccounts { get; set; }
       
    }
}