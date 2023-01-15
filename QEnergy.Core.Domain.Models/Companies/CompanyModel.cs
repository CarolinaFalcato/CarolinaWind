using QEnergy.Core.Domain.Models.Projects;
using QEnergy.Core.Domain.Models.Users;

namespace QEnergy.Core.Domain.Models.Companies
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Relationships
        public List<UserModel> Users { get; set; }
        public List<ProjectModel> Projects { get; set; }
    }
}

