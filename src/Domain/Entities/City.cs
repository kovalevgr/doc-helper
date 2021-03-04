using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DocHelper.Domain.Entities
{
    [Index(nameof(Name), nameof(Alias))]
    public class City
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Alias { get; set; }

        public ICollection<Specialty> Specialties { get; set; }
    }
}