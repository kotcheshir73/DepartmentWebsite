using LaboratoryHeadInterfaces.ViewModels;
using Models.LaboratoryHead;

namespace LaboratoryHeadImplementations
{
    public static class LaboratoryHeadModelFactoryToViewModel
    {
        public static MaterialTechnicalValueViewModel CreateMaterialTechnicalValueViewModel(MaterialTechnicalValue entity)
        {
            return new MaterialTechnicalValueViewModel
            {
                Id = entity.Id,
                DateInclude = entity.DateCreate,
                ClassroomId = entity.ClassroomId,
                Classroom = entity.Classroom.Number,
                InventoryNumber = entity.InventoryNumber,
                FullName = entity.FullName,
                Description = entity.Description,
                Location = entity.Location,
                Cost = entity.Cost,
                DateDelete = entity.DateDelete,
                DeleteReason = entity.DeleteReason
            };
        }

        public static MaterialTechnicalValueGroupViewModel CreateMaterialTechnicalValueGroupViewModel(MaterialTechnicalValueGroup entity)
        {
            return new MaterialTechnicalValueGroupViewModel
            {
                Id = entity.Id,
                GroupName = entity.GroupName,
                Order = entity.Order
            };
        }

        public static MaterialTechnicalValueRecordViewModel CreateMaterialTechnicalValueRecordViewModel(MaterialTechnicalValueRecord entity)
        {
            return new MaterialTechnicalValueRecordViewModel
            {
                Id = entity.Id,
                MaterialTechnicalValueId = entity.MaterialTechnicalValueId,
                InventoryNumber = entity.MaterialTechnicalValue.InventoryNumber,
                MaterialTechnicalValueGroupId = entity.MaterialTechnicalValueGroupId,
                GroupName = entity.MaterialTechnicalValueGroup.GroupName,
                GroupOrder = entity.MaterialTechnicalValueGroup.Order,
                FieldName = entity.FieldName,
                FieldValue = entity.FieldValue,
                Order = entity.Order
            };
        }

        public static SoftwareViewModel CreateSoftwareViewModel(Software entity)
        {
            return new SoftwareViewModel
            {
                Id = entity.Id,
                SoftwareName = entity.SoftwareName,
                SoftwareDescription = entity.SoftwareDescription,
                SoftwareKey = entity.SoftwareKey,
                SoftwareK = entity.SoftwareK
            };
        }

        public static SoftwareRecordViewModel CreateSoftwareRecordViewModel(SoftwareRecord entity)
        {
            return new SoftwareRecordViewModel
            {
                Id = entity.Id,
                DateSetup = entity.DateCreate,
                MaterialTechnicalValueId = entity.MaterialTechnicalValueId,
                SoftwareId = entity.SoftwareId,
                InventoryNumber = entity.MaterialTechnicalValue.InventoryNumber,
                SoftwareName = entity.Software.SoftwareName,
                SoftwareKey = entity.Software.SoftwareKey,
                SetupDescription = entity.SetupDescription,
                ClaimNumber = entity.ClaimNumber
            };
        }
    }
}