﻿using System;
using Tools.ViewModels;

namespace AcademicYearInterfaces.ViewModels
{
    public class DisciplineTimeDistributionRecordPageViewModel : PageSettingListViewModel<DisciplineTimeDistributionRecordViewModel> { }

    public class DisciplineTimeDistributionRecordViewModel : PageSettingElementViewModel
    {
        public Guid DisciplineTimeDistributionId { get; set; }

        public Guid TimeNormId { get; set; }

        public string DisciplineTimeDistribution { get; set; }

        public string TimeNorm { get; set; }

        public int WeekNumber { get; set; }

        public double Hours { get; set; }

        public string TimeNormHours { get; set; }
    }
}