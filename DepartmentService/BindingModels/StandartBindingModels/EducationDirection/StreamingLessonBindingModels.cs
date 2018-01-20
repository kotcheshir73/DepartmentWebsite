﻿using System;

namespace DepartmentService.BindingModels
{
    public class StreamingLessonGetBindingModel : PageSettingBinidingModel
	{
        public Guid? Id { get; set; }
	}

    public class StreamingLessonRecordBindingModel
    {
        public Guid Id { get; set; }

        public string IncomingGroups { get; set; }

        public string StreamName { get; set; }
    }
}
