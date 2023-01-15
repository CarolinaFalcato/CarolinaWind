using QEnergy.Core.Domain.Entities.Base;

namespace QEnergy.Core.Domain.Entities
{
    public class WindTurbineGenerator : EntityBase
    {
        // Properties
        public string Number { get; set; }

        // Relationships
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}