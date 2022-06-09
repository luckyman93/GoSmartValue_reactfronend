using System;
using System.ComponentModel.DataAnnotations;
using AV.Common.Entities;

namespace GoSmartValue.Web.ViewModels
{
    public class PhoneViewModel
    {
        public Guid Id { get; set; }
        public AccountType Type { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}