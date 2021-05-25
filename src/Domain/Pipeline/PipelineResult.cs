namespace DocHelper.Domain.Pipeline
{
    public class PipelineResult
    {
        public bool IsSuccess { get; set; }
        public CommonPayloadDto Result { get; set; }

        public PipelineResult(bool isSuccess = true)
        {
            IsSuccess = isSuccess;
        }
    }
}