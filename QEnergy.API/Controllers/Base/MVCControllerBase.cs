using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace QEnergy.API.Controllers.Base
{
    public class MVCControllerBase : Controller
    {
        protected readonly ILogger _logger;
        protected readonly IMediator _mediator;

        public MVCControllerBase(IMediator mediator, ILogger logger)
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

        protected Guid GetUserId()
        {
            return ControllerBaseUtil.GetUserId(User?.Identities);
        }

        protected int GetCompanyId()
        {
            return ControllerBaseUtil.GetCompanyId(User?.Identities);
        }
    }
}
