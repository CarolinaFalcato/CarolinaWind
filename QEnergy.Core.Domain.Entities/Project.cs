using QEnergy.Core.Domain.Entities.Base;
using QEnergy.Core.Domain.Models.Utils.Enums;

namespace QEnergy.Core.Domain.Entities
{
    public class Project : EntityBase
    {
        // Properties
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
        public Company Company { get; set; }
        //public ICollection<WindTurbineGenerator> WindTurbineGenerators { get; set; }
    }
}