using LaboratoryHeadInterfaces.BindingModels;
using Models.LaboratoryHead;

namespace LaboratoryHeadImplementations
{
    public static class LaboratoryHeadModelFacotryFromBindingModel
	{
        public static MaterialTechnicalValue CreateMaterialTechnicalValue(MaterialTechnicalValueSetBindingModel model, MaterialTechnicalValue entity = null)
        {
            if (entity == null)
            {
                entity = new MaterialTechnicalValue();
            }
            entity.DateCreate = model.DateInclude;
            entity.ClassroomId = model.ClassroomId;
            entity.InventoryNumber = model.InventoryNumber;
            entity.FullName = model.FullName;
            entity.Description = model.Description;
            entity.Location = model.Location;
            entity.Cost = model.Cost;
            entity.DeleteReason = model.DeleteReason;

            return entity;
        }

        public static MaterialTechnicalValueGroup CreateMaterialTechnicalValueGroup(MaterialTechnicalValueGroupSetBindingModel model, MaterialTechnicalValueGroup entity = null)
        {
            if (entity == null)
            {
                entity = new MaterialTechnicalValueGroup();
            }
            entity.GroupName = model.GroupName;
            entity.Order = model.Order;

            return entity;
        }

        public static MaterialTechnicalValueRecord CreateMaterialTechnicalValueRecord(MaterialTechnicalValueRecordSetBindingModel model, MaterialTechnicalValueRecord entity = null)
        {
            if (entity == null)
            {
                entity = new MaterialTechnicalValueRecord();
            }
            entity.MaterialTechnicalValueId = model.MaterialTechnicalValueId;
            entity.MaterialTechnicalValueGroupId = model.MaterialTechnicalValueGroupId;
            entity.FieldName = model.FieldName;
            entity.FieldValue = model.FieldValue;
            entity.Order = model.Order;

            return entity;
        }

        public static Software CreateSoftware(SoftwareSetBindingModel model, Software entity = null)
        {
            if (entity == null)
            {
                entity = new Software();
            }
            entity.SoftwareName = model.SoftwareName;
            entity.SoftwareDescription = model.SoftwareDescription;
            entity.SoftwareKey = model.SoftwareKey;
            entity.SoftwareK = model.SoftwareK;

            return entity;
        }

        public static SoftwareRecord CreateSoftwareRecord(SoftwareRecordSetBindingModel model, SoftwareRecord entity = null)
        {
            if (entity == null)
            {
                entity = new SoftwareRecord();
            }
            entity.DateCreate = model.DateSetup;
            entity.MaterialTechnicalValueId = model.MaterialTechnicalValueId;
            entity.SoftwareId = model.SoftwareId;
            entity.SetupDescription = model.SetupDescription;
            entity.ClaimNumber = model.ClaimNumber;

            return entity;
        }
    }
}