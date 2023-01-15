using QEnergy.Core.Domain.Models.Companies;
using QEnergy.Core.Domain.Models.Utils.Enums;

namespace QEnergy.Core.Domain.Models.Projects
{
    public class ProjectModel
    {
        // Properties
        public int? Id { get; set; }
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
        public bool Deleted { get; set; }

        // Relationships
        public int CompanyId { get; set; }
        public CompanyModel Company { get; set; }
        //public List<WindTurbineGeneratorModel> WindTurbineGenerators { get; set; }
    }
}