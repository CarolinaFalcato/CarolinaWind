using QEnergy.Core.Domain.Entities;
using QEnergy.Core.Domain.Models.Projects;

namespace QEnergy.Core.Domain.Mappers.Projects
{
    public class ProjectMapper
    {
        public static Project Map(ProjectModel model, Guid? userId, Project existingEntity = null)
        {
            Project result = existingEntity;
            if (model != null)
            {
                if (result == null)
                {
                    result = new Project();
                    result.CreatedBy = userId ?? default;
                }
                else
                {
                    result.Modified = DateTime.Now;
                    result.ModifiedBy = userId ?? default;
                }
                result.Name = model.Name;
                result.ProjectNumber = model.ProjectNumber;
                result.AcquisitionDate = model.AcquisitionDate;
                result.Number3lCode = model.Number3lCode;
                result.DealType = model.DealType;
                result.Group = model.Group;
                result.Status = model.Status;
                result.WTGNumbers = model.WTGNumbers;
                result.Kw = model.Kw;
                result.MonthsAquired = model.MonthsAquired;
                result.CompanyId = model.CompanyId;
            }
            return result;
        }

        public static ProjectModel UnMap(Project entity)
        {
            ProjectModel result = null;
            if (entity != null)
            {
                result = new ProjectModel();
                result.Id = entity.Id;
                result.Name = entity.Name;
                result.ProjectNumber = entity.ProjectNumber;
                result.AcquisitionDate = entity.AcquisitionDate;
                result.Number3lCode = entity.Number3lCode;
                result.DealType = entity.DealType;
                result.Group = entity.Group;
                result.Status = entity.Status;
                result.WTGNumbers = entity.WTGNumbers;
                result.Kw = entity.Kw;
                result.MonthsAquired = entity.MonthsAquired;
                result.CompanyId = entity.CompanyId;
            }
            return result;
        }
    }
}