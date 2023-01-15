using QEnergy.Core.Domain.Models.Projects;
using QEnergy.Core.Domain.Models.Utils.Enums;

namespace QEnergy.API.Models.Projects
{
    public class UpdateProjectRequest
    {
        public string Name { get; set; }
        public string ProjectNumber { get; set; }
        public DateTime? AcquisitionDate { get; set; }
        public string Number3lCode { get; set; }
        public ProjectDealTypes DealType { get; set; }
        public string Group { get; set; }
        public ProjectStatuses Status { get; set; }
        public string WTGNumbers { get; set; }
        public long? Kw { get; set; }
        public int? MonthsAquired { get; set; }

        public ProjectModel ToProjectModel(ProjectModel existingProject)
        {
            var result = new ProjectModel
            {
                Id = existingProject.Id,
                Name = string.IsNullOrWhiteSpace(Name) ? existingProject.Name : Name,
                ProjectNumber = string.IsNullOrWhiteSpace(ProjectNumber) ? existingProject.Name : ProjectNumber,
                AcquisitionDate = AcquisitionDate,
                Number3lCode = string.IsNullOrWhiteSpace(Number3lCode) ? existingProject.Name : Number3lCode,
                DealType = DealType,
                Group = string.IsNullOrWhiteSpace(Group) ? existingProject.Name : Group,
                Status = Status,
                WTGNumbers = string.IsNullOrWhiteSpace(Name) ? existingProject.Name : WTGNumbers,
                Kw = Kw,
                MonthsAquired = MonthsAquired,
                CompanyId = existingProject.CompanyId
            };
            return result;
        }
    }
}
