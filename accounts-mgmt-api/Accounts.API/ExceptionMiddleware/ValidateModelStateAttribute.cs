using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Accounts.Common.HelperDTO;
using Accounts.Common.Constants;

namespace Accounts.ExceptionMiddleware
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToList();

                var responseObj = new ExceptionWrapperDTO
                {
                    StatusCode = ExceptionStatusCodesMessage.BadRequestCode,
                    StatusDescription = ExceptionStatusCodesMessage.BadRequestCodeDesc,
                    Message = ExceptionStatusCodesMessage.BadRequestMessage,
                    Errors = errors
                };
                context.Result = new BadRequestObjectResult(JsonConvert.SerializeObject(responseObj));
            }
        }
    }
}
