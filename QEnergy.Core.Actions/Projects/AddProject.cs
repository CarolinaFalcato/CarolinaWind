using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QEnergy.Core.Actions.Base;
using QEnergy.Core.Domain.Entities;
using QEnergy.Core.Domain.Mappers.Projects;
using QEnergy.Core.Domain.Models.Projects;
using QEnergy.Core.Domain.Models.Utils.Enums;
using QEnergy.Services.Persistence.EntityFramework;

namespace QEnergy.Core.Actions.Projects
{
    public class AddProject : RequestBase<ProjectModel>
    {
        public AddProject(ProjectModel project, Guid userId, int userCompanyId)
            : base(userId, userCompanyId)
        {
            Project = project;
        }
        public ProjectModel Project { get; }
    }

    public class AddProjectHandler : RequestHandlerBase<AddProject, ProjectModel>
    {
        public AddProjectHandler(QEnergyDbContext context, ILogger<AddProjectHandler> logger) : base(logger, context: context)
        {
        }

        protected override async Task<ProjectModel> DoHandle(AddProject message)
        {
            ProjectModel result = null;
            bool entityAlreadyExists = false;
            entityAlreadyExists = await context.Set<Project>()
                .AsNoTracking()
                .AnyAsync(x => (x.Deleted == false && x.ProjectNumber.ToUpper() == message.Project.ProjectNumber.ToUpper() && x.AcquisitionDate == message.Project.AcquisitionDate
                        && x.Number3lCode.ToUpper() == message.Project.Number3lCode.ToUpper() && x.Group.ToUpper() == message.Project.Group.ToUpper()));

            if (!entityAlreadyExists)
            {
                var project = ProjectMapper.Map(message.Project, message.UserId);
                project.Status = ProjectStatuses.Aquisition;

                await context.AddAsync(project);
                await context.SaveChangesAsync();
                result = ProjectMapper.UnMap(project);
                logger.LogInformation("New project added with Id: {0} ", result.Id);
            }
            else
                logger.LogInformation("A project already exists with same project number, acquisition date, 3l code and group");
            return result;
        }
    }
}
