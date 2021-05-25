using System;
using System.Collections.Generic;

namespace DocHelper.Domain.Pipeline
{
    public abstract class BasePipeline
    {
        public abstract IReadOnlyList<IPipelineStep> Steps { get; }
        public abstract CommonPipelineDto PipelineDto { get; }

        public CommonPayloadDto PayloadDto { get; private set; } = new CommonPayloadDto();
        
        event Action<object> Finished;

        protected virtual void OnFinished(object obj)
        {
            Finished?.Invoke(obj);
        }
    }
}