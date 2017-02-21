﻿using DepartmentDAL.Enums;

namespace DepartmentDAL.Models
{
    /// <summary>
    /// Класс, описывающий занятие в семестре
    /// </summary>
    public class SemesterRecord : ScheduleRecord
    {
        public int Week { get; set; }

        public int Day { get; set; }

        public int Lesson { get; set; }

        public LessonTypes LessonType { get; set; }

        /// <summary>
        /// Является ли пара потоковой
        /// </summary>
        public bool IsStreaming { get; set; }
    }
}
