using System.Threading.Tasks;

namespace DocHelper.Domain.Pipeline
{
    public interface IPipelineStep
    {
         Task Execute(CommonPayloadDto payload);
    }
}