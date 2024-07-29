using System.Net;

namespace WalkAPI.Middleware
{
    public class ExceptionHandleMiddleware
    {
        private readonly ILogger<ExceptionHandleMiddleware> logger;
        private readonly RequestDelegate _next;

        public ExceptionHandleMiddleware(ILogger<ExceptionHandleMiddleware> logger,
            RequestDelegate _next)
        {
            this.logger = logger;
            this._next = _next;
        }

        public async Task InvokeAsync(HttpContext httpContent)
        {
            try
            {
                await _next(httpContent);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                //Log this exx
                logger.LogError(ex, $"{errorId} : {ex.Message}");

                //return the cusstomer respond
                httpContent.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContent.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong!  Need to looking to resolve!!"
                };
                await httpContent.Response.WriteAsJsonAsync(error);

            }
        }
    }

}