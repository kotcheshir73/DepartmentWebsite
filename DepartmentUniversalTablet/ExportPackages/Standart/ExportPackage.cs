using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentUniversalTablet.ExportPackages.Standart
{
    /*
    1. Выбираем год обучения
    2. Направление
    3. Дисциплина
    4. Тип занятий(лек, лаб)
    5. Занятие
    6. Группа или подгруппа
    7. Ввод списком отметок(например, для лекций), либо выбор студента для ввода отметки
    */
   [Export(typeof(IExportPackage))]
    class ExportPackage : IExportPackage
    {
        public string Discipline => "Standart";

        public string Lecturer => "Standart";

        public Type GetUI => typeof(TimeNormsPage);
    }
}
