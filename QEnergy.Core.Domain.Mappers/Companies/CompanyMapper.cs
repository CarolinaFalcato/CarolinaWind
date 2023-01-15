using QEnergy.Core.Domain.Entities;
using QEnergy.Core.Domain.Models.Companies;

namespace QEnergy.Core.Domain.Mappers.Companies
{
    public static class CompanyMapper
    {
        public static Company Map(CompanyModel model, Guid? userId, Company existingEntity = null)
        {
            Company result = existingEntity;
            if (model != null)
            {
                if (result == null)
                {
                    result = new Company();
                    result.CreatedBy = userId ?? default;
                }
                else
                {
                    result.ModifiedBy = userId ?? default;
                    result.Modified = DateTime.Now;
                }
                result.Id = model.Id;
                result.Name = model.Name;
            }
            return result;
        }

        public static CompanyModel UnMap(Company entity)
        {
            CompanyModel result = null;
            if (entity != null)
            {
                result = new CompanyModel()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                };
            }
            return result;
        }
    }
}
