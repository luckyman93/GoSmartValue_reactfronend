using AV.Contracts.Enums;
using System;

namespace AV.Contracts.Models.Valuation
{
    public class InstructionAppointmentModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid UserId { get; set; }
        public Users.UserModel User { get; set; }
        public DateTime Date { get; set; }
        public AppointmentStatus Status { get; set; }
        public string Comment { get; set; }
    }
}