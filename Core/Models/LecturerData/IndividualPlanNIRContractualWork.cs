﻿using Models.Base;
using System;
using System.Runtime.Serialization;

namespace Models.LecturerData
{
    /// <summary>
    /// Класс, описывающий НИР(научные статьи) преподавателей
    /// </summary>
    [DataContract]
    public class IndividualPlanNIRContractualWork : BaseEntity
    {
        [DataMember]
        public Guid LecturerId { get; set; }
        
        [DataMember]
        public string JobContent { get; set; }

        [DataMember]
        public string Post { get; set; }

        [DataMember]
        public string PlannedTerm { get; set; }

        [DataMember]
        public string ReadyMark { get; set; }

        //-------------------------------------------------------------------------

        public virtual Lecturer Lecturer { get; set; }

        //-------------------------------------------------------------------------
    }
}