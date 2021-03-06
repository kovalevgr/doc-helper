﻿using System;
using System.Threading.Tasks;
using DocHelper.Domain.Pipeline;
using DocHelper.Infrastructure.Pipeline.Builder;
using Microsoft.Extensions.Logging;

namespace DocHelper.Infrastructure.Pipeline.Executor
{
    public class PipelineExecutor : IPipelineExecutor
    {
        private readonly ILogger<PipelineExecutor> _logger;
        private readonly PipelineBuilder _builder;

        public PipelineExecutor(ILogger<PipelineExecutor> logger, PipelineBuilder builder)
        {
            _logger = logger;
            _builder = builder;
        }

        public async Task<PipelineResult> Execute(CommonPipelineDto dto)
        {
            var result = new PipelineResult();
            var pipeline = _builder.BuildPipelineByDto(dto);
            foreach (var pipelineStepType in pipeline.Steps)
            {
                try
                {
                    var step = _builder.GetStepByType(pipelineStepType);

                    await step.Execute(pipeline.PayloadDto);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    result.IsSuccess = false;
                }
                finally
                {
                    result.Result = pipeline.PayloadDto;
                }
            }

            return result;
        }
    }
}