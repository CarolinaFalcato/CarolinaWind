using MediatR;
using Microsoft.Extensions.Logging;
using QEnergy.Services.Persistence.EntityFramework;
using System.Diagnostics;

namespace QEnergy.Core.Actions.Base
{
    public abstract class RequestBase<T> : IRequest<T>
    {
        public RequestBase(Guid userId, int userCompanyId)
        {
            UserId = userId;
            UserCompanyId = userCompanyId;
        }
        public Guid UserId { get; }
        public int UserCompanyId { get; }
    }

    public abstract class RequestHandlerBase<T, U> : IRequestHandler<T, U> where T : RequestBase<U>
    {
        protected readonly ILogger logger;
        protected readonly QEnergyDbContext context;
        protected RequestHandlerBase(ILogger logger, QEnergyDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task<U> Handle(T message, CancellationToken cancellationToken)
        {
            U? result;
            try
            {
                result = await DoHandle(message);
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format("Error in {0}. Error: {1}", new StackTrace().GetFrame(0).GetMethod().Name, ex.ToString()));
                throw;
            }
            return result;
        }

        protected abstract Task<U> DoHandle(T message);
    }
}