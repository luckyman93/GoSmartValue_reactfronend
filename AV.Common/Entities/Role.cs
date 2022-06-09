using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AV.Common.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public IEnumerable<UserRole> Users { get; set; }
    }
}