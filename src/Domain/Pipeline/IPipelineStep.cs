namespace DocHelper.Domain.Pipeline
{
    public interface IPipelineStep
    {
        void Execute(CommonPayloadDto payload);
    }
}