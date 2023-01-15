using System.ComponentModel.DataAnnotations;

namespace QEnergy.Core.Domain.Models.Authorization
{
    public class AuthorizationModel
    {
        [Required]
        public string ApiKey { get; set; }
        [Required]
        public string ApiSecret { get; set; }
    }
}
