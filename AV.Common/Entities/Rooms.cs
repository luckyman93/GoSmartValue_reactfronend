using AV.Contracts.Enums;
using System;

namespace AV.Common.Entities
{
    public class Rooms
    {
        public Guid Id { get; set; }
        public RoomType RoomType { get; set; }
    }
}