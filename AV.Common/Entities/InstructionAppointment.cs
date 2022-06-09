using AV.Contracts.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class InstructionAppointment : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public AppointmentStatus Status { get; set; }
        public string Comment { get; set; }
    }
}