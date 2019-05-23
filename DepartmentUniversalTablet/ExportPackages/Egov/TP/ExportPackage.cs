using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentUniversalTablet.ExportPackages.Egov.TP
{
    /*
    1. Авторизация
    2. Учебный год
    3. Направление
    4. Дисциплина
    5. Семестр
    //Тут переход в стандартный модуль
        ??????
    */
    [Export(typeof(IExportPackage))]
    class ExportPackage : IExportPackage
    {
        public string Discipline => "Технологии программирования";

        public string Lecturer => "Эгов Е.Н.";

        public Type GetUI => typeof(TimeNormsPage);
    }
}
