using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentUniversalTablet
{
    class ImportManager
    {
        [ImportMany("IExportPackage")]
        public IEnumerable<IExportPackage> extCollection { get; set; }

        public ImportManager()
        {
            ContainerConfiguration containerConfiguration = new ContainerConfiguration();
            containerConfiguration.WithAssembly(typeof(IExportPackage).Assembly);
            var compositionHost = containerConfiguration.CreateContainer();
            extCollection = compositionHost.GetExports<IExportPackage>();
        }
    }
}
