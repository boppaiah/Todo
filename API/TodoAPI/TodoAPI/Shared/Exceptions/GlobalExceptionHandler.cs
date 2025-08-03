using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Shared.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var exceptionMessage = "";
            DateTime exceptionOccured = DateTime.Now;
            var occurDate = exceptionOccured.Date;
            var occurTime = exceptionOccured.TimeOfDay;

            _logger.LogError(exception.Message, $"Error Message:{exceptionMessage} at Date: {occurDate} on time :{occurTime}");

            (string Details, string Title, int StatusCoode) = exception switch
            {
                InternalServerException => (
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError),

                NotFoundException => (
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound),

                BadRequestException => (
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest),

                ValidationException => (
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest),

                _ => (
                    exception.Message,
                    exception.GetType().Name,
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest)

            };


            var problemDetails = new ProblemDetails
            {
                Title = Title,
                Status = StatusCoode,
                Detail = Details,
                Instance = httpContext.Request.Path,

            };
            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);
            if (exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("ValidationErrors: ", validationException.Errors);
            }

            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;

        }
    }
}
