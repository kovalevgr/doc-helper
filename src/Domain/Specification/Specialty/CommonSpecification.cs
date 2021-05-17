using Ardalis.Specification;

namespace DocHelper.Domain.Specification.Specialty
{
    public class CommonSpecification : Specification<Entities.Specialty>
    {
        public CommonSpecification()
        {
            Query
                .Where(s => s.Alias == "nevrolog");
        }
    }
}