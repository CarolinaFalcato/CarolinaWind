using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QEnergy.Core.Actions.Base;
using QEnergy.Core.Domain.Entities;
using QEnergy.Core.Domain.Mappers.Companies;
using QEnergy.Core.Domain.Mappers.Projects;
using QEnergy.Core.Domain.Models.Projects;
using QEnergy.Services.Persistence.EntityFramework;

namespace QEnergy.Core.Actions.Projects
{
    public class GetProjectById : RequestBase<ProjectModel>
    {
        public GetProjectById(int projectId, bool includeRelatedEntities, Guid userId, int userCompanyId)
            : base(userId, userCompanyId)
        {
            ProjectId = projectId;
            IncludeRelatedEntities = includeRelatedEntities;
        }
        public int ProjectId { get; }
        public bool IncludeRelatedEntities { get; }
    }

    public class GetProjectByIdHandler : RequestHandlerBase<GetProjectById, ProjectModel>
    {
        public GetProjectByIdHandler(QEnergyDbContext context, ILogger<GetProjectByIdHandler> logger) : base(logger, context: context)
        {
        }

        protected override async Task<ProjectModel> DoHandle(GetProjectById message)
        {
            ProjectModel result = null;

            logger.LogInformation("GetProject for Id: {0}. IncludeRelatedEntities: {1}", message.ProjectId, message.IncludeRelatedEntities);

            var query = context.Set<Project>().AsQueryable();
            if (message.IncludeRelatedEntities)
                query = query.Include(x => x.Company);

            var entity = await query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == message.ProjectId && !x.Deleted);
            if (entity != null)
            {
                result = ProjectMapper.UnMap(entity);
                if (message.IncludeRelatedEntities)
                {
                    result.Company = CompanyMapper.UnMap(entity.Company);
                }

                logger.LogInformation("GetProject for Id: {0} finished", message.ProjectId);
            }
            else
                logger.LogWarning("Project with Id: {0} not found", message.ProjectId);

            return result;
        }
    }
}
