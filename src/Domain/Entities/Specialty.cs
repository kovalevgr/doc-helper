namespace DocHelper.Domain.Entities
{
    public class Specialty : BaseEntity
    {
        public string Title { get; set; }

        public string Alias { get; set; }
        
        public City City { get; set; }
    }
}