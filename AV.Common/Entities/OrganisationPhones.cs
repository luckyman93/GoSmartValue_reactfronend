using System;

namespace AV.Common.Entities
{
    public class OrganisationPhones
    {
        public Guid OrganisationId { get; set; }
        public virtual Organisation Organisation { get; set; }
        public Guid PhoneId { get; set; }
        public virtual Phone Phone { get; set; }
    }
}