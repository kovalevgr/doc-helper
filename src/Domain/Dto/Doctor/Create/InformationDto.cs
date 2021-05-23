using DocHelper.Domain.Enums;

namespace DocHelper.Domain.Dto.Doctor.Create
{
    public class InformationDto
    {
        public InformationType Type { get; set; }
        public string Title { get; set; }
        public int Priority { get; set; }
    }
}