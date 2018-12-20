﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, описывающий зачет на зачетной неделе
    /// </summary>
    [DataContract]
    public class OffsetRecord : ScheduleRecord
    {
        [Required]
        [DataMember]
        public int Week { get; set; }

        [Required]
        [DataMember]
        public int Day { get; set; }

        [Required]
        [DataMember]
        public int Lesson { get; set; }

        /// <summary>
        /// Является ли пара потоковой
        /// </summary>
        [DataMember]
        public bool IsStreaming { get; set; }

        /// <summary>
        /// При загрузке расписания отметка, проверен зачет или нет
        /// </summary>
        [NotMapped]
        public bool Checked { get; set; }

        public OffsetRecord() : base()
        {
            Checked = false;
        }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------
    }
}
