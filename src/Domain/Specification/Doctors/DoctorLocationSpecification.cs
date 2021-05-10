using System.Linq;
using Ardalis.Specification;
using DocHelper.Domain.Entities.DoctorAggregate;
using DocHelper.Domain.ValueObjects;

namespace DocHelper.Domain.Specification.Doctors
{
    public class DoctorLocationSpecification : Specification<Doctor>
    {
        public DoctorLocationSpecification(Location location)
        {
            Query
                .Include(d => d.Specialties.Where(ds => ds.Specialty.City.Name == location.CurrentLocation));
        }
    }
}