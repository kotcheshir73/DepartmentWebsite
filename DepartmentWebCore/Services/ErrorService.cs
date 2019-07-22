using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentWebCore.Services
{
    public static class ErrorService
    {
        public static void ThrowErrors(List<KeyValuePair<string, string>> errors)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach(var error in errors)
            {
                stringBuilder.Append($"{error.Key}{error.Value}<br />");
            }

            throw new Exception(stringBuilder.ToString());
        }
    }
}