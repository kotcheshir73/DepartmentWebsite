using System.Collections.Generic;

namespace AcademicYearInterfaces.HelperModels
{
    public class BlueAsteriskNewHour
    {
        public int Kurs { get; set; }

        public int Semester { get; set; }

        public string ObjectCode { get; set; }

        public string TypeNormCode { get; set; }

        public Dictionary<string, decimal> TimeNorms { get; set; }
    }
}