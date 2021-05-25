using System.Collections.Generic;
using System.Linq;
using DocHelper.Domain.Pipeline;
using DocHelper.Infrastructure.Pipeline.Exceptions;

namespace DocHelper.Infrastructure.Pipeline.Builder
{
    public class PipelineBuilder
    {
        private readonly IEnumerable<BasePipeline> _pipelines;

        public PipelineBuilder(IEnumerable<BasePipeline> pipelines)
        {
            _pipelines = pipelines;
        }

        public BasePipeline BuildByDto(CommonPipelineDto dto)
        {
            var pipeline = _pipelines.FirstOrDefault(p => p.PipelineDto.GetType() == dto.GetType());
            if (pipeline is null)
            {
                throw new PipelineNotFoundException();
            }

            return pipeline;
        }
    }
}