using System.Net;

namespace EmployeeManagement.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Proceed with the next middleware in the pipeline
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //// Handle exception globally
                //await HandleExceptionAsync(httpContext, ex);

                // Log the exception
                _logger.LogError(ex, "An unexpected error occurred.");

                // Handle the exception and show a user-friendly error message
                httpContext.Response.Redirect("/Error?message=" + Uri.EscapeDataString("Something went wrong. Please try again later."));

            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred");

            // Customize the response based on the exception type
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                message = "An unexpected error occurred. Please try again later.",
                details = exception.Message // You can include the exception details here if needed
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
