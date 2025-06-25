using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.API.ExceptionHandling
{
    public class WarehouseGlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<WarehouseGlobalExceptionHandler> _logger;

        public WarehouseGlobalExceptionHandler(ILogger<WarehouseGlobalExceptionHandler> logger) => _logger = logger;

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(
                exception, $"Exception handled in Global Excpetion Handler: {exception.Message}", exception.StackTrace);

            var problemDetails = new ProblemDetails
            {
                Status = httpContext.Response.StatusCode,
                Title = $"Error: {exception.Message}",
                Type = exception.GetType().Name,
                Detail = "See more details in the logs."
            };

            await httpContext
                .Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);


            return true;
        }
    }
}
