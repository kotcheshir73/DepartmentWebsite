using Enums;
using Models.AcademicYearData;
using Models.Attributes;
using Models.Examination;
using Models.LecturerData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Models.Base
{
    /// <summary>
    /// Класс, описывающий преподавателя
    /// </summary>
    [DataContract]
    [ClassUse("LecturerPost", "LecturerPostId", "У преподавателя есть должность")]
    public class Lecturer : BaseEntity
    {
        [Required]
        [DataMember]
        public Guid LecturerPostId { get; set; }

        [MaxLength(20)]
        [Required]
        [DataMember]
        public string FirstName { get; set; }
        
        [MaxLength(30)]
        [Required]
        [DataMember]
        public string LastName { get; set; }
        
        [MaxLength(30)]
        [Required]
        [DataMember]
        public string Patronymic { get; set; }
        
		[MaxLength(10)]
        [DataMember]
        public string Abbreviation { get; set; }
        
        [Required]
        [DataMember]
        public DateTime DateBirth { get; set; }
        
        [MaxLength(250)]
        [Required]
        [DataMember]
        public string Address { get; set; }
        
        [MaxLength(150)]
        [Required]
        [DataMember]
        public string Email { get; set; }
        
        [MaxLength(50)]
        [Required]
        [DataMember]
        public string MobileNumber { get; set; }
        
        [MaxLength(50)]
        [DataMember]
        public string HomeNumber { get; set; }
        
        [Required]
        [DataMember]
        public Post Post { get; set; }

        [Required]
        [DataMember]
        public Rank Rank { get; set; }

        [Required]
        [DataMember]
        public Rank2 Rank2 { get; set; }

        [DataMember]
        public string Description { get; set; }
        
        [DataMember]
        public byte[] Photo { get; set; }

        //-------------------------------------------------------------------------

        public virtual LecturerPost LecturerPost { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("LecturerId")]
        public virtual List<AcademicPlanRecordMission> AcademicPlanRecordMissions { get; set; }

        [ForeignKey("LecturerId")]
        public virtual List<LecturerWorkload> LecturerWorkloads { get; set; }

        [ForeignKey("LecturerId")]
        public virtual List<Statement> Statements { get; set; }

        [ForeignKey("LecturerId")]
        public virtual List<ExaminationList> ExaminationLists { get; set; }

        [ForeignKey("LecturerId")]
        public virtual List<IndividualPlan> IndividualPlans { get; set; }

        //-------------------------------------------------------------------------

        public override string ToString()
        {
            StringBuilder result = new StringBuilder(LastName);
            if(!string.IsNullOrEmpty(FirstName))
            {
                result.Append(" ");
                result.Append(FirstName[0]);
                result.Append(".");
            }
            if (!string.IsNullOrEmpty(Patronymic))
            {
                result.Append(" ");
                result.Append(Patronymic[0]);
                result.Append(".");
            }
            return result.ToString();
        }
    }
}