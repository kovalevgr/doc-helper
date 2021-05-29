using System;
using System.Collections.Generic;

namespace DocHelper.Domain.Pipeline
{
    public abstract class BasePipeline
    {
        public abstract IReadOnlyList<Type> Steps { get; }
        public abstract CommonPipelineDto PipelineDto { get; }

        public virtual CommonPayloadDto PayloadDto { get; } = new CommonPayloadDto();
        
        event Action<object> Finished;

        protected virtual void OnFinished(object obj)
        {
            Finished?.Invoke(obj);
        }
    }
}