using System;

namespace Interfaces.BindingModels
{
    public class PageSettingGetBinidingModel
    {
        public Guid? Id { get; set; }

        public int? PageNumber { get; set; }

		public int? PageSize { get; set; }
	}
}
