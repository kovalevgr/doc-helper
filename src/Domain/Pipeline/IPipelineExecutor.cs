using System.Threading.Tasks;

namespace DocHelper.Domain.Pipeline
{
    public interface IPipelineExecutor
    {
        Task<PipelineResult> Execute(CommonPipelineDto dto);
    }
}