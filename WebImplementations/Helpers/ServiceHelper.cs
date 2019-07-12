using Enums;
using Models.AcademicYearData;
using System;
using System.Linq;
using Tools;

namespace WebImplementations.Helpers
{
    internal static class ServiceHelper
    {
        /// <summary>
        /// Получение текущего учебного года.
        /// Если он не задан, то берется последний созданный
        /// </summary>
        /// <returns></returns>
        public static ResultService<AcademicYear> GetCurrentAcademicYear()
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var currentSetting = context.CurrentSettings.FirstOrDefault(cs => cs.Key == "Учебный год");
                    AcademicYear academicYear = null;
                    if (currentSetting == null)
                    {
                        academicYear = context.AcademicYears.OrderByDescending(x => x.DateCreate).FirstOrDefault();
                    }
                    else
                    {
                        academicYear = context.AcademicYears.Where(x => x.Title == currentSetting.Value).FirstOrDefault();
                    }

                    if (academicYear == null)
                    {
                        return ResultService<AcademicYear>.Error("Error:", "CurrentSetting not found", ResultServiceStatusCode.NotFound);
                    }

                    return ResultService<AcademicYear>.Success(academicYear);
                }
            }
            catch (Exception ex)
            {
                return ResultService<AcademicYear>.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}