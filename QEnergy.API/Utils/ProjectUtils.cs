using QEnergy.Core.Domain.Models.Utils.Enums;

namespace QEnergy.API.Utils
{
    public static class ProjectUtils
    {
        public static IconDescription GetStatusIconDescription(ProjectStatuses projectStatus)
        {
            IconDescription result = null;
            result = new IconDescription();
            switch (projectStatus)
            {
                case ProjectStatuses.Aquisition:
                    result.Icon = "main-color fa-solid fa-file-contract";
                    result.Description = TextResources.TextResources.ProjectStatuses_Aquisition;
                    break;
                case ProjectStatuses.InDevelopment:
                    result.Icon = "text-warning fa-solid fa-users-gear";
                    result.Description = TextResources.TextResources.ProjectStatuses_InDevelopment;
                    break;
                case ProjectStatuses.Operating:
                    result.Icon = "text-success fas fa-circle-check";
                    result.Description = TextResources.TextResources.ProjectStatuses_Operating;
                    break;
            }
            return result;
        }

        public static IconDescription GetDealTypeIconDescription(ProjectDealTypes projectDealType)
        {
            IconDescription result = null;
            result = new IconDescription();
            switch (projectDealType)
            {
                case ProjectDealTypes.Asset:
                    result.Icon = "main-color fa-solid fa-magnifying-glass-dollar";
                    result.Description = TextResources.TextResources.ProjectDealTypes_Asset;
                    break;
                case ProjectDealTypes.Share:
                    result.Icon = "main-color fa-regular fa-handshake";
                    result.Description = TextResources.TextResources.ProjectDealTypes_Share;
                    break;
            }
            return result;
        }
    }
}
