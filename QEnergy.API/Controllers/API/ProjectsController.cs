using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QEnergy.API.Controllers.Base;
using QEnergy.API.Models.Projects;
using QEnergy.Core.Actions.Projects;
using QEnergy.Core.Domain.Models.Projects;
using System.Net;
using System.Net.Mime;

namespace QEnergy.API.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/qenergy/projects")]

    public class ProjectsController : ApiControllerBase
    {

        public ProjectsController(IMediator mediator, ILogger<ProjectsController> logger)
            : base(mediator, logger)
        {

        }

        /// <summary>
        /// Gets the projects list
        /// </summary>
        /// <remarks>
        /// Gets the projects list
        /// </remarks>
        /// <returns>The list of all projects not deleted</returns>
        /// <response code="200">The list of all projects not deleted</response>   
        /// <response code="400">Invalid query parameters</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("list")]
        [ProducesResponseType(typeof(List<ProjectModel>), (int)HttpStatusCode.OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Get()
        {
            var result = await TryCatch(() => _mediator.Send(new GetProjects(GetUserId(), GetCompanyId())));
            if (result != null)
                return Ok(result);
            else
                return BadRequest();
        }

        /// <summary>
        /// Add a new project
        /// </summary>
        /// <remarks>
        /// Add a new project
        /// </remarks>
        /// <param name="createProjectRequest">The new project data</param>
        /// <returns>The created project identifier</returns>
        /// <response code="200">Returns the created project identifier</response>   
        /// <response code="400">Invalid query parameters</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Add([FromBody][BindRequired] CreateProjectRequest createProjectRequest)
        {
            if (createProjectRequest == null)
                return BadRequest("Project data is null");

            if (createProjectRequest.HasModelErrors())
                return BadRequest("Missing mandatory fields");

            var projectModel = createProjectRequest.ToProjectModel();
            var result = await TryCatch(() => _mediator.Send(new AddProject(projectModel, GetUserId(), GetCompanyId())));
            if (result != null)
                return Ok(result.Id);
            else
                return BadRequest();
        }

        /// <summary>
        /// Get a specific project by Id
        /// </summary>
        /// <remarks>
        /// Get a specific project by Id
        /// </remarks>
        /// <param name="id">The project identifier</param>
        /// <returns>The project corresponding with the identifier</returns>
        /// <response code="200">The project corresponding with the identifier</response>   
        /// <response code="400">Invalid query parameters</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Get([BindRequired] int id)
        {
            if (id == default)
                return BadRequest("Invalid project identifier");
            if (!ModelState.IsValid)
            {
                var message = GetModelStateErrorMessage(ModelState);
                return BadRequest(message);
            }
            var result = await TryCatch(() => _mediator.Send(new GetProjectById(id, true, GetUserId(), GetCompanyId())));
            if (result != null)
                return Ok(result);
            else
                return BadRequest();
        }

        /// <summary>
        /// Update a project
        /// </summary>
        /// <remarks>
        /// Update a project
        /// </remarks>
        /// <param name="id">The project identifier</param>
        /// <param name="updateProjectRequest">The project updated data</param>
        /// <returns>A boolean with the result of the operation</returns>
        /// <response code="200">Returns a boolean with the result of the operation</response>   
        /// <response code="400">Invalid query parameters</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Update([BindRequired] int id, [FromBody][BindRequired] UpdateProjectRequest updateProjectRequest)
        {
            if (id == default)
                return BadRequest("Invalid project identifier");
            if (updateProjectRequest == null)
                return BadRequest("Project data is null");

            var existingProject = await TryCatch(() => _mediator.Send(new GetProjectById(id, false, GetUserId(), GetCompanyId())));
            if (existingProject == null)
                return NotFound("Project not found");

            if (!ModelState.IsValid)
            {
                var message = GetModelStateErrorMessage(ModelState);
                return BadRequest(message);
            }

            var projectModel = updateProjectRequest.ToProjectModel(existingProject);
            var result = await TryCatch(() => _mediator.Send(new UpdateProject(id, projectModel, GetUserId(), GetCompanyId())));
            if (result)
                return Ok(result);
            else
                return BadRequest();
        }

        /// <summary>
        /// Remove a project
        /// </summary>
        /// <remarks>
        /// Remove a project
        /// </remarks>
        /// <param name="id">The project identifier</param>
        /// <returns>A boolean with the result of the operation</returns>
        /// <response code="200">Returns a boolean with the result of the operation</response>   
        /// <response code="400">Invalid query parameters</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Delete([BindRequired] int id)
        {
            if (id == default)
                return BadRequest("Invalid project identifier");

            var result = await TryCatch(() => _mediator.Send(new DeleteProject(id, GetUserId(), GetCompanyId())));
            if (result)
                return Ok(result);
            else
                return BadRequest();
        }
    }
}
