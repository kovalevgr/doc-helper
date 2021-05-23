namespace DocHelper.Domain.Entities.DoctorAggregate
{
    public class Stats : BaseEntity
    {
        public double Rating { get; private set; }
        public int CountComments { get; private set; }
        public int CountLikes { get; private set; }
        public int CountDisLikes { get; private set; }

        public Doctor Doctor { get; private set; }
        public int DoctorId { get; set; }

        private Stats()
        {
        }
        
        public Stats(Doctor doctor)
        {
            Doctor = doctor;
        }

        public Stats(double rating, int countComments, int countLikes, int countDisLikes, Doctor doctor)
        {
            Rating = rating;
            CountComments = countComments;
            CountLikes = countLikes;
            CountDisLikes = countDisLikes;
            Doctor = doctor;
        }
    }
}