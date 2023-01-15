using QEnergy.Core.Domain.Entities.Base;

namespace QEnergy.Core.Domain.Entities
{
    public class Company : EntityBase
    {
        public string Name { get; set; }

        //Relationships
        public ICollection<User> Users { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
