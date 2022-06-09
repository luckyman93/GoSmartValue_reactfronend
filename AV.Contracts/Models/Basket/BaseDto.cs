using System;

namespace AV.Contracts.Models.Basket
{
    public class BaseDto
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}