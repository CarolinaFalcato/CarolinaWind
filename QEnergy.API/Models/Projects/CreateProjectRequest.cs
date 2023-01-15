using QEnergy.Core.Domain.Models.Projects;
using QEnergy.Core.Domain.Models.Utils.Enums;
using System.ComponentModel.DataAnnotations;

namespace QEnergy.API.Models.Projects
{
    public class CreateProjectRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ProjectNumber { get; set; }
        public DateTime? AcquisitionDate { get; set; }
        [Required]
        public string Number3lCode { get; set; }
        [Required]
        public ProjectDealTypes DealType { get; set; }
        [Required]
        public string Group { get; set; }
        [Required]
        public ProjectStatuses Status { get; set; }
        [Required]
        public string WTGNumbers { get; set; }
        public long? Kw { get; set; }
        public int? MonthsAquired { get; set; }
        [Required]
        public int CompanyId { get; set; }

        public bool HasModelErrors()
        {
            bool result = string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(ProjectNumber) || string.IsNullOrWhiteSpace(Number3lCode) || string.IsNullOrWhiteSpace(Group) || string.IsNullOrWhiteSpace(WTGNumbers) || CompanyId == default;
            return result;
        }

        public ProjectModel ToProjectModel()
        {
            var result = new ProjectModel
            {
                Id = null,
                Name = Name,
                ProjectNumber = ProjectNumber,
                AcquisitionDate = AcquisitionDate,
                Number3lCode = Number3lCode,
                DealType = DealType,
                Group = Group,
                Status = Status,
                WTGNumbers = WTGNumbers,
                Kw = Kw,
                MonthsAquired = MonthsAquired,
                CompanyId = CompanyId
            };
            return result;
        }
    }
}
