using QEnergy.Core.Domain.Models.Projects;

namespace QEnergy.Core.Domain.Models.WindTurbineGenerators
{
    public class WindTurbineGeneratorModel
    {
        // Properties
        public int Id { get; set; }
        public string Numbers { get; set; }
        public int Kw { get; set; }
        public int? MonthsAquired { get; set; }

        // Relationships
        public int ProjectId { get; set; }
        public ProjectModel Project { get; set; }
    }
}