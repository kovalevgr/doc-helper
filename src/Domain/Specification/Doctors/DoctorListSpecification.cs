using System.Linq;
using Ardalis.Specification;
using DocHelper.Domain.Entities.DoctorAggregate;

namespace DocHelper.Domain.Specification.Doctors
{
    public class DoctorListSpecification : Specification<Doctor>
    {
        public DoctorListSpecification(string districtAlias, string specialtyAlias)
        {
            if (specialtyAlias is not null)
            {
                Query
                    .Include(
                        d => d.Specialties.Where(ds => ds.Specialty.City.Alias == specialtyAlias));
            }
        }
    }
}