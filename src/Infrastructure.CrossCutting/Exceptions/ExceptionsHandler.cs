namespace Infrastructure.CrossCutting.Exceptions
{

    using System.Net;
    using FluentValidation;
    using FluentValidation.Internal;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

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
            
            await httpContext.Response.WriteAsync(exception.Message, cancellationToken);

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