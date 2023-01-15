using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace QEnergy.API.Controllers.Base
{
    public class ApiControllerBase : ControllerBase
    {
        protected readonly ILogger _logger;
        protected readonly IMediator _mediator;
        public ApiControllerBase(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        protected async Task<TResult> TryCatch<TResult>(Func<Task<TResult>> f)
        {
            try
            {
                return await f();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Error in {0}. Error: {1}", new StackTrace().GetFrame(0).GetMethod().Name, ex.ToString()));
                throw;
            }
        }

        protected int GetCompanyId()
        {
            return ControllerBaseUtil.GetCompanyId(User?.Identities);
        }

        protected Guid GetUserId()
        {
            return ControllerBaseUtil.GetUserId(User?.Identities);
        }

        protected string GetModelStateErrorMessage(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        {
            return string.Join(" | ", modelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
        }
    }
}

