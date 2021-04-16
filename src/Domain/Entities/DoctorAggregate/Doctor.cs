using System.Collections.Generic;
using System.Linq;
using DocHelper.Domain.Interfaces;

namespace DocHelper.Domain.Entities.DoctorAggregate
{
    public class Doctor : BaseEntity, IAggregateRoot
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MiddleName { get; private set; }
        public string Titles { get; private set; }
        public int WorkExperience { get; private set; }
        public string Description { get; private set; }
        public string Photo { get; private set; }

        public Stats Stats { get; private set; }

        public ICollection<Information> DoctorInformations { get; private set; }

        private Doctor()
        {
        }

        public Doctor(string firstName, string lastName, string middleName, string titles, int workExperience,
            string description, string photo)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Titles = titles;
            WorkExperience = workExperience;
            Description = description;
            Photo = photo;
        }

        public Doctor(string firstName, string lastName, string middleName, string titles, int workExperience,
            string description, string photo, Stats stats, List<Information> informations)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Titles = titles;
            WorkExperience = workExperience;
            Description = description;
            Photo = photo;
            Stats = stats;
            DoctorInformations = informations;
        }

        public void AddInformation(Information information)
        {
            if (DoctorInformations.All(i => i.Id != information.Id))
            {
                DoctorInformations.Add(information);
            }
        }
    }
}