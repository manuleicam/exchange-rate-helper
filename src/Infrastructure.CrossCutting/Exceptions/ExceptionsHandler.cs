namespace Infrastructure.CrossCutting.Exceptions
{
    using FluentValidation;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class ExceptionsHandler : IExceptionHandler
    {
        private static readonly Dictionary<Type, int> exceptionToStatusMap = new Dictionary<Type, int>();
        private readonly ILogger<ExceptionsHandler> logger;

        public ExceptionsHandler(ILogger<ExceptionsHandler> logger)
        {
            this.logger = logger;
            
            exceptionToStatusMap[typeof(ArgumentException)] = StatusCodes.Status400BadRequest;
            exceptionToStatusMap[typeof(NullReferenceException)] = StatusCodes.Status500InternalServerError;
            exceptionToStatusMap[typeof(DomainModelException)] = StatusCodes.Status400BadRequest;
            exceptionToStatusMap[typeof(ResourceAlreadyExist)] = StatusCodes.Status400BadRequest;
            exceptionToStatusMap[typeof(ValidationException)] = StatusCodes.Status400BadRequest;
            exceptionToStatusMap[typeof(ExternalCallException)] = StatusCodes.Status400BadRequest;
            exceptionToStatusMap[typeof(NotSameResourceException)] = StatusCodes.Status400BadRequest;
            exceptionToStatusMap[typeof(ResourceNotFound)] = StatusCodes.Status404NotFound;
        }
        
        private int GetStatusForException(Exception exception)
        {
            if (exceptionToStatusMap.TryGetValue(exception.GetType(), out int statusCode))
            {
                return statusCode;
            }
            else
            {
                this.logger.LogError(
                    null,
                    $"ExceptionNotMapped. ExceptionType: {exception.GetType()}. ExceptionMessage: {exception.Message}",
                    new
                    {
                        ExceptionType = exception.GetType(),
                        ExceptionMessage = exception.Message
                    });
                
                return StatusCodes.Status500InternalServerError;
            }
        }
        
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var statusCode = this.GetStatusForException(exception);

            httpContext.Response.StatusCode = statusCode;

            var exceptionBody = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                Message = exception.Message,
            });
            
            await httpContext.Response.WriteAsync(exceptionBody, cancellationToken);

            if (statusCode >= StatusCodes.Status400BadRequest)
            {
                this.logger.LogError(
                    null,
                    $"An exception was thrown, StatusCode: {statusCode}, ExceptionType {exception.GetType()}, ExceptionMessage: {exception.Message} ",
                    new
                    {
                        StatusCode = statusCode,
                        ExceptionType = exception.GetType(),
                        ExceptionMessage = exception.Message
                    });
            }

            return true;
        }
    }
}