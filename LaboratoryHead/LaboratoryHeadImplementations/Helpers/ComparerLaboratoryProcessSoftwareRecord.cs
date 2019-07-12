using LaboratoryHeadInterfaces.ViewModels;
using System.Collections.Generic;

namespace LaboratoryHeadImplementations.Helpers
{
    public class ComparerLaboratoryProcessSoftwareRecord : IEqualityComparer<LaboratoryProcessSoftwareRecordsViewModels>
    {
        public bool Equals(LaboratoryProcessSoftwareRecordsViewModels x, LaboratoryProcessSoftwareRecordsViewModels y)
        {
            if (x.DateSetup != y.DateSetup)
            {
                return false;
            }
            if (x.SoftwareName != y.SoftwareName)
            {
                return false;
            }
            if (x.SoftwareDescription != y.SoftwareDescription)
            {
                return false;
            }
            if (x.SoftwareKey != y.SoftwareKey)
            {
                return false;
            }
            if (x.ClaimNumber != y.ClaimNumber)
            {
                return false;
            }
            return true;
        }

        public int GetHashCode(LaboratoryProcessSoftwareRecordsViewModels obj)
        {
            if (obj == null) return 0;

            int hashSoftwareName = string.IsNullOrEmpty(obj.SoftwareName) ? 0 : obj.SoftwareName.GetHashCode();

            int hashSoftwareKey = string.IsNullOrEmpty(obj.SoftwareKey) ? 0 : obj.SoftwareKey.GetHashCode();

            int hashClaimNumber = string.IsNullOrEmpty(obj.ClaimNumber) ? 0 : obj.ClaimNumber.GetHashCode();

            int hashDateStart = obj.DateSetup.GetHashCode();

            return hashSoftwareName ^ hashSoftwareKey ^ hashClaimNumber ^ hashDateStart;
        }
    }
}