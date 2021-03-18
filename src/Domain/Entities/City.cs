using System.Collections.Generic;

namespace DocHelper.Domain.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        
        public string Alias { get; set; }

        public ICollection<Specialty> Specialties { get; set; }
    }
}