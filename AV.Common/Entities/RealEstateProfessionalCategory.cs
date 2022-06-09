using System.ComponentModel.DataAnnotations;

namespace AV.Common.Entities
{
    public class RealEstateProfessionalCategory
    {
        [Key] 
        public string CategoryName { get; set; }
    }
}