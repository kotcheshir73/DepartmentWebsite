﻿using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentUniversalTablet.ExportPackages.Standart
{
    /*
    1. Авторизация
    2. Учебный год
    3. Направление
    4. Дисциплина
    5. Семестр
    //Тут переход в стандартный модуль
    6. TimeNormsPage
    7. StudentGroupsPage
    8. DisciplineLessonsPage
    9. Студенты
    10. DisciplineLessonConductedStudentPage
    */
    [Export(typeof(IExportPackage))]
    class ExportPackage : IExportPackage
    {
        public string Discipline => "Standart";

        public string Lecturer => "Standart";

        public Type GetUI => typeof(StudentGroupsPage);
    }
}
