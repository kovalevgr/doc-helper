using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DocHelper.Domain.Entities
{
    [Index(nameof(Alias), nameof(Title))]
    public class Specialty
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Alias { get; set; }
        
        public City City { get; set; }
    }
}