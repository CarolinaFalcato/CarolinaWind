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
    public class GetProjects : RequestBase<List<ProjectModel>>
    {
        public GetProjects(Guid userId, int userCompanyId)
            : base(userId, userCompanyId)
        {
        }
        public Guid KYBVerificationId { get; }
    }

    public class GetProjectsHandler : RequestHandlerBase<GetProjects, List<ProjectModel>>
    {
        public GetProjectsHandler(QEnergyDbContext context, ILogger<GetProjectsHandler> logger) : base(logger, context: context)
        {
        }

        protected override async Task<List<ProjectModel>> DoHandle(GetProjects message)
        {
            List<ProjectModel> result = null;
            var entities = await context.Set<Project>()
                .Include(x => x.Company)
                .Where(x => !x.Deleted)
                .ToListAsync();

            if (entities?.Any() ?? false)
                result = MapProjects(entities);

            return result;
        }

        private static List<ProjectModel> MapProjects(ICollection<Project> entities)
        {
            List<ProjectModel> result = new();
            if (entities?.Any() ?? false)
            {
                foreach (var director in entities)
                {
                    var directorModel = ProjectMapper.UnMap(director);
                    if (director.Company != null)
                    {
                        if (director.Company != null)
                            directorModel.Company = CompanyMapper.UnMap(director.Company);
                    }
                    result.Add(directorModel);
                }
            }
            return result;
        }
    }
}
