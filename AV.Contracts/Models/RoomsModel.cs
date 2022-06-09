using AV.Contracts.Enums;
using System;

namespace AV.Contracts.Models
{
    public class RoomsModel
    {
        public Guid Id { get; set; }
        public RoomType RoomType { get; set; }
    }
}
