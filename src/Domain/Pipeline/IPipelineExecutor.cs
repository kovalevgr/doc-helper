namespace DocHelper.Domain.Pipeline
{
    public interface IPipelineExecutor
    {
        PipelineResult Execute(CommonPipelineDto dto);
    }
}