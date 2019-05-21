using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentUniversalTablet
{
    public interface IExportPackage
    {
        string Discipline { get; }
        string Lecturer { get; }
        Type GetUI { get; }
    }
}
