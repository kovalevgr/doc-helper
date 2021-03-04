using DocHelper.Domain.Common.Mappings;
using DocHelper.Domain.Entities;

namespace DocHelper.Domain.Dto
{
    public class SpecialtyDto : IMapFrom<Specialty>
    {
        public string Title { get; set; }
        public string Alias { get; set; }
    }
}