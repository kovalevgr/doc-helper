using DocHelper.Domain.Common.Mappings;
using DocHelper.Domain.Entities.DoctorAggregate;

namespace DocHelper.Domain.Dto
{
    public class StatsDto : IMapFrom<Stats>
    {
        public double Rating { get; set; }
        public int CountComments { get; set; }
        public int CountLikes { get; set; }
        public int CountDisLikes { get; set; }
    }
}