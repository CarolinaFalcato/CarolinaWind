using System.ComponentModel.DataAnnotations;

namespace QEnergy.Core.Domain.Entities
{
    public class User
    {
        // Properties
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        // Relationships
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}