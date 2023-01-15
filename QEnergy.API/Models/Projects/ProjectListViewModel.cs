using QEnergy.API.Utils;
using QEnergy.Core.Domain.Models.Projects;

namespace QEnergy.API.Models.Projects
{
    public class ProjectListViewModel : ProjectListModel
    {
        public IconDescription DealTypeIconDescription => ProjectUtils.GetDealTypeIconDescription(DealType);
        public IconDescription StatusIconDescription => ProjectUtils.GetStatusIconDescription(Status);

        public ProjectListViewModel()
        {
        }
    }
}
