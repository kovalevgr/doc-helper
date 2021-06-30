using System.Collections.Generic;
using DocHelper.Domain.Enums;
using JetBrains.Annotations;

namespace DocHelper.Domain.Documents
{
    public class Doctor : BaseDocument
    {
        public DoctorDocument DoctorDocument { get; protected set; }
    }

    public class DoctorDocument
    {
        public string Alias { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Titles { get; set; }
        public int WorkExperience { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }

        [CanBeNull] public StatsDocument Stats { get; set; }

        public List<InformationDocument> Informations { get; set; }
        public List<SpecialtyDocument> Specialties { get; set; }

        public DoctorDocument()
        {
            Informations = new List<InformationDocument>();
            Specialties = new List<SpecialtyDocument>();
        }
    }

    public class StatsDocument
    {
        public double Rating { get; set; }
        public int CountComments { get; set; }
        public int CountLikes { get; set; }
        public int CountDisLikes { get; set; }
    }

    public class InformationDocument
    {
        public InformationType Type { get; set; }
        public string Title { get; set; }
        public int Priority { get; set; }
    }

    public class SpecialtyDocument
    {
        public string Title { get; set; }
        public string Alias { get; set; }
    }
}