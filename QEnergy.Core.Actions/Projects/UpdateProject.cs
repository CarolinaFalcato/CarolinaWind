using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QEnergy.Core.Actions.Base;
using QEnergy.Core.Domain.Entities;
using QEnergy.Core.Domain.Mappers.Projects;
using QEnergy.Core.Domain.Models.Projects;
using QEnergy.Services.Persistence.EntityFramework;

namespace QEnergy.Core.Actions.Projects
{
    public class UpdateProject : RequestBase<bool>
    {
        public UpdateProject(int projectId, ProjectModel project, Guid userId, int userCompanyId)
            : base(userId, userCompanyId)
        {
            ProjectId = projectId;
            Project = project;
        }
        public int ProjectId { get; }
        public ProjectModel Project { get; }
    }

    public class UpdateProjectHandler : RequestHandlerBase<UpdateProject, bool>
    {
        public UpdateProjectHandler(QEnergyDbContext context, ILogger<UpdateProjectHandler> logger) : base(logger, context: context)
        {
        }
        protected override async Task<bool> DoHandle(UpdateProject message)
        {
            bool result = false;
            var entity = await context.Set<Project>()
                        .Where(x => x.Id == message.ProjectId)
                        .FirstOrDefaultAsync();
            if (entity != null)
            {
                entity = ProjectMapper.Map(message.Project, message.UserId, entity);

                context.Update(entity);
                await context.SaveChangesAsync();
                result = true;
                logger.LogInformation("Updated project with Id: {0} ", message.Project.Id);
            }
            else
                logger.LogInformation("Project with Id: {0} not found", message.Project.Id);
            return result;
        }
    }
}
