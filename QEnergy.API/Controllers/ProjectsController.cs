using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using QEnergy.API.Controllers.Base;
using QEnergy.API.Models.Projects;
using QEnergy.API.Utils.Consts;
using QEnergy.Core.Actions.Projects;
using QEnergy.Core.Domain.Mappers.Utils;
using QEnergy.Core.Domain.Utils;

namespace QEnergy.API.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ProjectsController : MVCControllerBase
    {

        public ProjectsController(IMediator mediator, ILogger<ProjectsController> logger) : base(mediator, logger)
        {
        }

        public async Task<ActionResult> List(string sortField, int? pageNumber)
        {
            ViewData = await GetViewData(pageNumber ?? 1, sortField);
            var result = await GetProjectsViewModelList(pageNumber ?? 1, sortField);
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateProjectsList(
            int? pageNumber,
            string sortField,
            string projectNameFilter = null,
            string projectNumberFilter = null,
            string dateFromFilter = null,
            string dateToFilter = null)
        {
            ViewData = await GetViewData(pageNumber ?? 1, sortField, projectNameFilter, projectNumberFilter, dateFromFilter, dateToFilter);
            var result = await GetProjectsViewModelList(pageNumber ?? 1, sortField, projectNameFilter, projectNumberFilter, dateFromFilter, dateToFilter);
            return PartialView("ListTableResult", result);
        }

        [HttpPost]
        public async Task<ActionResult> RemoveProject(int projectId)
        {
            var result = await _mediator.Send(new DeleteProject(projectId, GetUserId(), GetCompanyId()));
            return Ok(result);
        }

        private async Task<PaginatedList<ProjectListViewModel>> GetProjectsViewModelList(
            int pageNumber,
            string sortField = "Id",
            string nameFilter = null,
            string projectNumberFilter = null,
            string dateFromFilter = null,
            string dateToFilter = null)
        {
            PaginatedList<ProjectListViewModel> result = null;
            var projectsModelList = await _mediator.Send(new GetProjectsList(GetUserId(), GetCompanyId(), pageNumber, Consts.MAX_RESULTS_PER_PAGE, sortField, false, nameFilter, projectNumberFilter, dateFromFilter, dateToFilter));
            if (projectsModelList?.Any() ?? false)
            {
                List<ProjectListViewModel> faceCheckViewModelList = new();
                projectsModelList.ForEach(x => faceCheckViewModelList.Add(GenericMapper.Map(x, new ProjectListViewModel())));
                result = new PaginatedList<ProjectListViewModel>(faceCheckViewModelList, projectsModelList.TotalItems, pageNumber, Consts.MAX_RESULTS_PER_PAGE);
            }

            return result;
        }

        private async Task<ViewDataDictionary> GetViewData(
            int pageNumber,
            string sortField = "Id",
            string nameFilter = null,
            string projectNumberFilter = null,
            string dateFromFilter = null,
            string dateToFilter = null)
        {
            ViewData["CurrentSort"] = sortField;
            ViewData["CurrentPage"] = pageNumber;
            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortField) ? "Id" : "";
            ViewData["NameSortParm"] = sortField == "Name" ? "name_desc" : "Name";
            ViewData["ProjectNumberSortParm"] = sortField == "ProjectNumber" ? "projectNumber_desc" : "ProjectNumber";
            ViewData["AcquisitionDateSortParm"] = sortField == "AcquisitionDate" ? "acquisitionDate_desc" : "AcquisitionDate";
            ViewData["Number3lCodeSortParm"] = sortField == "Number3lCode" ? "number3lCode_desc" : "Number3lCode";
            ViewData["DealTypeSortParm"] = sortField == "DealType" ? "dealType_desc" : "DealType";
            ViewData["GroupSortParm"] = sortField == "Group" ? "group_desc" : "Group";
            ViewData["StatusSortParm"] = sortField == "Status" ? "status_desc" : "Status";
            ViewData["CompanyIdSortParm"] = sortField == "CompanyId" ? "companyId_desc" : "CompanyId";

            ViewData["NameFilter"] = nameFilter;
            ViewData["ProjectFilter"] = projectNumberFilter;
            ViewData["DateFromFilter"] = dateFromFilter;
            ViewData["DateToFilter"] = dateToFilter;

            return ViewData;
        }

        public async Task<IActionResult> GetWTGModal(int id)
        {
            ProjectViewModel result = null;
            var projectModel = await _mediator.Send(new GetProjectById(id, true, GetUserId(), GetCompanyId()));
            if (projectModel != null)
            {
                result = new ProjectViewModel();
                result = GenericMapper.Map(projectModel, result);
            }

            return PartialView("WtgModal", result);
        }
    }
}