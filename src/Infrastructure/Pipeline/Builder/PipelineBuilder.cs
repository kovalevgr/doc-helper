using System;
using System.Collections.Generic;
using System.Linq;
using DocHelper.Domain.Pipeline;
using DocHelper.Infrastructure.Pipeline.Exceptions;

namespace DocHelper.Infrastructure.Pipeline.Builder
{
    public class PipelineBuilder
    {
        private readonly IEnumerable<BasePipeline> _pipelines;
        private readonly IEnumerable<IPipelineStep> _steps;

        public PipelineBuilder(IEnumerable<BasePipeline> pipelines, IEnumerable<IPipelineStep> pipelineSteps)
        {
            _pipelines = pipelines;
            _steps = pipelineSteps;
        }

        public BasePipeline BuildPipelineByDto(CommonPipelineDto dto)
        {
            return _pipelines.FirstOrDefault(p => p.PipelineDto.GetType() == dto.GetType()) ??
                   throw new PipelineNotFoundException();
        }

        public IPipelineStep GetStepByType(Type type)
        {
            return _steps.FirstOrDefault(s => s.GetType() == type) ??
                   throw new PipelineStepNotFoundException();
        }
    }
}