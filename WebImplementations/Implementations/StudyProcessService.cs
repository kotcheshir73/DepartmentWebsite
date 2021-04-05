using System;
using System.Collections.Generic;
using System.Linq;
using WebInterfaces.Interfaces;
using System.ComponentModel;

namespace WebImplementations.Implementations
{
    public class StudyProcessService : IStudyProcessService
    {
        public (List<string> displayNames, List<string> propertiesNames) GetPropertiesNames(Type type)
        {
            List<string> displayNames = new List<string>();
            List<string> propertiesNames = new List<string>();

            type.GetProperties().ToList().ForEach(x =>
            {
                object[] dn = x.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                if (dn.Length > 0)
                {
                    displayNames.Add((dn.FirstOrDefault() as DisplayNameAttribute).DisplayName);
                    propertiesNames.Add(x.Name);
                }
            });

            (List<string> displayNames, List<string> propertiesNames) info = (displayNames, propertiesNames);

            return info;
        }

        public List<List<object>> GetPropertiesValues<T>(List<T> list, List<string> propertiesNames)
        {
            List<List<object>> result = new List<List<object>>();

            foreach (T element in list)
            {
                result.Add(new List<object> { element.GetType().GetProperty("Id").GetValue(element, null) });
                foreach (string propertyName in propertiesNames)
                {
                    object value = element.GetType().GetProperty(propertyName).GetValue(element, null);
                    if (value is bool)
                    {
                        if ((bool)value == true)
                        {
                            result.LastOrDefault().Add("да");
                        }
                        else
                        {
                            result.LastOrDefault().Add("нет");
                        }
                    }
                    else
                    {
                        result.LastOrDefault().Add(value);
                    }
                }
            }

            return result;
        }
    }
}
