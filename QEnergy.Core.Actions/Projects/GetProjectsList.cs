using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QEnergy.Core.Actions.Base;
using QEnergy.Core.Domain.Entities;
using QEnergy.Core.Domain.Models.Projects;
using QEnergy.Core.Domain.Utils;
using QEnergy.Services.Persistence.EntityFramework;
using System.Diagnostics;
using System.Globalization;

namespace QEnergy.Core.Actions.Projects
{
    public class GetProjectsList : RequestBase<PaginatedList<ProjectListModel>>
    {
        public GetProjectsList(Guid userId, int userCompanyId, int pageNumber, int pageSize, string sortField, bool includeRelatedEntities, string nameFilter, string projectNumberFilter, string dateFromFilter, string dateToFilter)
            : base(userId, userCompanyId)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            SortField = sortField;
            IncludeRelatedEntities = includeRelatedEntities;
            NameFilter = nameFilter;
            ProjectNumberFilter = projectNumberFilter;
            DateFromFilter = dateFromFilter;
            DateToFilter = dateToFilter;
        }

        public int PageSize { get; }
        public int PageNumber { get; }
        public string SortField { get; }
        public bool IncludeRelatedEntities { get; }
        public string NameFilter { get; }
        public string ProjectNumberFilter { get; }
        public string DateFromFilter { get; }
        public string DateToFilter { get; }
        public int CompanyId { get; }
    }

    public class GetProjectsListHandler : RequestHandlerBase<GetProjectsList, PaginatedList<ProjectListModel>>
    {
        public GetProjectsListHandler(QEnergyDbContext context, ILogger<GetProjectsListHandler> logger) : base(logger, context: context)
        {
        }

        protected override async Task<PaginatedList<ProjectListModel>> DoHandle(GetProjectsList message)
        {
            PaginatedList<ProjectListModel> result = null;
            try
            {
                var query = context.Set<Project>().AsQueryable().AsNoTracking();
                if (message.IncludeRelatedEntities)
                    query = query.Include(x => x.Company);

                if (!string.IsNullOrEmpty(message.NameFilter))
                {
                    var nameNormalized = message.NameFilter.ToUpper().Trim();
                    query = query.Where(x => x.Name.ToUpper().Contains(nameNormalized));
                }
                if (!string.IsNullOrEmpty(message.ProjectNumberFilter))
                {
                    var projectNumberNormalized = message.ProjectNumberFilter.ToUpper().Trim();
                    query = query.Where(x => x.ProjectNumber.ToUpper().Contains(projectNumberNormalized));
                }
                if (!string.IsNullOrEmpty(message.DateFromFilter))
                {
                    if (DateTime.TryParseExact(message.DateFromFilter, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateFrom))
                        query = query.Where(x => x.AcquisitionDate >= dateFrom);
                }
                if (!string.IsNullOrEmpty(message.DateToFilter))
                {
                    if (DateTime.TryParseExact(message.DateToFilter, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTo))
                        query = query.Where(x => x.AcquisitionDate <= dateTo);
                }

                query = query.Where(x => !x.Deleted);

                if (!string.IsNullOrWhiteSpace(message.SortField))
                {
                    query = message.SortField switch
                    {
                        "Id" => query.OrderBy(x => x.Id),
                        "Name" => query.OrderBy(x => x.Name),
                        "name_desc" => query.OrderByDescending(x => x.Name),
                        "ProjectNumber" => query.OrderBy(x => x.ProjectNumber),
                        "projectNumber_desc" => query.OrderByDescending(x => x.ProjectNumber),
                        "AcquisitionDate" => query.OrderBy(x => x.AcquisitionDate),
                        "acquisitionDate_desc" => query.OrderByDescending(x => x.AcquisitionDate),
                        "Number3lCode" => query.OrderBy(x => x.Number3lCode),
                        "number3lCode_desc" => query.OrderByDescending(x => x.Number3lCode),
                        "DealType" => query.OrderBy(x => x.DealType),
                        "dealType_desc" => query.OrderByDescending(x => x.DealType),
                        "Group" => query.OrderBy(x => x.Group),
                        "group_desc" => query.OrderByDescending(x => x.Group),
                        "Status" => query.OrderBy(x => x.Status),
                        "status_desc" => query.OrderByDescending(x => x.Status),
                        "CompanyId" => query.OrderBy(x => x.CompanyId),
                        "companyId_desc" => query.OrderByDescending(x => x.CompanyId),
                        _ => query.OrderByDescending(x => x.Id),
                    };
                }
                else
                    query = query.OrderByDescending(x => x.Created);

                int totalItems = query.Count();
                int skipRows = (message.PageNumber - 1) * message.PageSize;
                var entities = await query.Skip(skipRows).Take(message.PageSize).Select(x => new ProjectListModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ProjectNumber = x.ProjectNumber,
                    AcquisitionDate = x.AcquisitionDate,
                    Number3lCode = x.Number3lCode,
                    DealType = x.DealType,
                    Group = x.Group,
                    Status = x.Status,
                    CompanyId = x.CompanyId,
                    CompanyName = x.Company != null ? x.Company.Name : null,
                    WTGNumbers = x.WTGNumbers,
                    Kw = x.Kw,
                    MonthsAquired = x.MonthsAquired,
                }).ToListAsync();

                if (entities?.Any() ?? false)
                    result = PaginatedList<ProjectListModel>.Create(entities, message.PageNumber, message.PageSize, totalItems);
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
