using Value;

namespace DocHelper.Domain.ValueObjects
{
    public class Location : ValueObject
    {
        private const string DefaultLocationName = "kiev";
        
        public string CurrentLocation { get; set; }

        public static Location GetCurrentLocation()
        {
            return new ()
            {
                CurrentLocation = DefaultLocationName
            };
        }
    }
}