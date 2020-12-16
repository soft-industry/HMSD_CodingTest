using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApps.EncriptionService.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var ex = context.Exception;

            if (ex != null)
            {
                var jsonRes = new JsonResult(new Models.ErrorResult
                {
                    ErrorMsg = ex.Message,
                    InnerErrorMsg = ex.InnerException?.Message 
                });

                jsonRes.StatusCode = 500;
                context.Result = jsonRes;
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
    }
}
