using CustomerInfo.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CustomerInfo.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {

            _logger.LogError(context.Exception, "An exception occurred.");
            // Redirect to a custom error page with a 500 status code
            context.Result = new RedirectResult("/Error/500");
            context.ExceptionHandled = true;
        }
    }
}
