using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QEnergy.Core.Actions.Base;
using QEnergy.Core.Domain.Entities;
using QEnergy.Services.Persistence.EntityFramework;
using System.Diagnostics;

namespace QEnergy.Core.Actions.Projects
{
    public class DeleteProject : RequestBase<bool>
    {
        public DeleteProject(int projectId, Guid userId, int userCompanyId)
            : base(userId, userCompanyId)
        {
            ProjectId = projectId;
        }
        public int ProjectId { get; }
    }

    public class DeleteProjectHandler : RequestHandlerBase<DeleteProject, bool>
    {
        public DeleteProjectHandler(QEnergyDbContext context, ILogger<DeleteProjectHandler> logger) : base(logger, context)
        {
        }

        protected override async Task<bool> DoHandle(DeleteProject message)
        {
            bool result = false;
            logger.LogInformation("Removing Project with Id: {0}", message.ProjectId);

            try
            {
                var entity = await context.Set<Project>()
                        .Where(x => x.Id == message.ProjectId)
                        .FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.Deleted = true;
                    entity.ModifiedBy = message.UserId;
                    context.Update(entity);
                    await context.SaveChangesAsync();
                    result = true;
                }
                else
                    logger.LogWarning("Project with Id: {0} not found", message.ProjectId);
            }
            catch (Exception ex)
            {
                logger.LogError("Error in {0}. Error: {1}", new StackTrace().GetFrame(0).GetMethod().Name, ex.ToString());
                throw;
            }

            return result;
        }
    }
}
