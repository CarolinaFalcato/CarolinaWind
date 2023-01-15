using System.ComponentModel.DataAnnotations;

namespace QEnergy.Core.Domain.Entities.Base
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public Guid CreatedBy { get; set; } // Creation user id 
        public Guid? ModifiedBy { get; set; } // Modification user id

        public EntityBase()
        {
            Created = DateTime.UtcNow;
        }

        public void SetModified(Guid userId)
        {
            ModifiedBy = userId;
            Modified = DateTime.UtcNow;
        }
    }
}
