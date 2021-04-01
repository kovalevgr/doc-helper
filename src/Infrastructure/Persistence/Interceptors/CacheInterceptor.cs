using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using DocHelper.Infrastructure.Cache.Processor;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DocHelper.Infrastructure.Persistence.Interceptors
{
    public class CacheInterceptor : DbCommandInterceptor
    {
        private readonly ICacheProcessor _cacheProcessor;

        public CacheInterceptor(ICacheProcessor cacheProcessor)
        {
            _cacheProcessor = cacheProcessor;
        }

        public override int NonQueryExecuted(
            DbCommand command,
            CommandExecutedEventData eventData,
            int result)
        {
            if (eventData?.Context is null)
            {
                return base.NonQueryExecuted(command, eventData, result);
            }

            return _cacheProcessor.ProcessExecutedCommands(command, eventData.Context, result);
        }

        public override ValueTask<int> NonQueryExecutedAsync(
            DbCommand command,
            CommandExecutedEventData eventData,
            int result,
            CancellationToken cancellationToken = default)
        {
            if (eventData?.Context is null)
            {
                return base.NonQueryExecutedAsync(command, eventData, result, cancellationToken);
            }

            return new ValueTask<int>(_cacheProcessor.ProcessExecutedCommands(command, eventData.Context, result));
        }

        public override InterceptionResult<int> NonQueryExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<int> result)
        {
            if (eventData?.Context is null)
            {
                return base.NonQueryExecuting(command, eventData, result);
            }

            return _cacheProcessor.ProcessExecutingCommands(command, eventData.Context, result);
        }

        public override ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData?.Context is null)
            {
                return base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);
            }

            return new ValueTask<InterceptionResult<int>>(
                _cacheProcessor.ProcessExecutingCommands(command, eventData.Context, result));
        }

        public override DbDataReader ReaderExecuted(
            DbCommand command,
            CommandExecutedEventData eventData,
            DbDataReader result)
        {
            if (eventData?.Context is null)
            {
                return base.ReaderExecuted(command, eventData, result);
            }

            return _cacheProcessor.ProcessExecutedCommands(command, eventData.Context, result);
        }


        public override ValueTask<DbDataReader> ReaderExecutedAsync(
            DbCommand command,
            CommandExecutedEventData eventData,
            DbDataReader result,
            CancellationToken cancellationToken = default)
        {
            if (eventData?.Context is null)
            {
                return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
            }

            return new ValueTask<DbDataReader>(
                _cacheProcessor.ProcessExecutedCommands(command, eventData.Context, result));
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            if (eventData?.Context is null)
            {
                return base.ReaderExecuting(command, eventData, result);
            }

            return _cacheProcessor.ProcessExecutingCommands(command, eventData.Context, result);
        }

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData?.Context is null)
            {
                return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
            }

            return new ValueTask<InterceptionResult<DbDataReader>>(
                _cacheProcessor.ProcessExecutingCommands(command, eventData.Context, result));
        }

        public override object ScalarExecuted(
            DbCommand command,
            CommandExecutedEventData eventData,
            object result)
        {
            if (eventData?.Context is null)
            {
                return base.ScalarExecuted(command, eventData, result);
            }

            return _cacheProcessor.ProcessExecutedCommands(command, eventData.Context, result);
        }

        public override ValueTask<object> ScalarExecutedAsync(
            DbCommand command,
            CommandExecutedEventData eventData,
            object result,
            CancellationToken cancellationToken = default)
        {
            if (eventData?.Context is null)
            {
                return base.ScalarExecutedAsync(command, eventData, result, cancellationToken);
            }

            return new ValueTask<object>(_cacheProcessor.ProcessExecutedCommands(command, eventData.Context, result));
        }

        public override InterceptionResult<object> ScalarExecuting(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<object> result)
        {
            if (eventData?.Context is null)
            {
                return base.ScalarExecuting(command, eventData, result);
            }

            return _cacheProcessor.ProcessExecutingCommands(command, eventData.Context, result);
        }

        public override ValueTask<InterceptionResult<object>> ScalarExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<object> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData?.Context is null)
            {
                return base.ScalarExecutingAsync(command, eventData, result, cancellationToken);
            }

            return new ValueTask<InterceptionResult<object>>(
                _cacheProcessor.ProcessExecutingCommands(command, eventData.Context, result));
        }
    }
}