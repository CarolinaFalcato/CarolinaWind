using System.ComponentModel;

namespace QEnergy.Core.Domain.Mappers.Utils
{
    public static class GenericMapper
    {
        public static T Map<T, U>(U source, T target) where T : class, new()
                                              where U : class, new()
        {
            if (source != null && target != null)
            {
                PropertyDescriptorCollection sourceproperties = TypeDescriptor.GetProperties(new U());
                PropertyDescriptorCollection targetproperties = TypeDescriptor.GetProperties(new T());
                foreach (PropertyDescriptor pd in targetproperties)
                    foreach (PropertyDescriptor _pd in sourceproperties)
                        if (pd.Name == _pd.Name)
                            pd.SetValue(target, _pd.GetValue(source));
            }
            return target;
        }
    }
}
