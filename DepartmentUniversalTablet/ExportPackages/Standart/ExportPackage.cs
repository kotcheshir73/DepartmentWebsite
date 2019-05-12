using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentUniversalTablet.ExportPackages.Standart
{
    [Export(typeof(IExportPackage))]
    class ExportPackage : IExportPackage
    {
        public string Discipline => "Standart";

        public string Lecturer => "Standart";

        public Type GetUI => typeof(TypeLessonPage);
    }
}
