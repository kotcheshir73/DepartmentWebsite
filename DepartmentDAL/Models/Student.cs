using DepartmentDAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentDAL.Models
{
    public class Student
    {
        [Key]
        [MaxLength(10)]
        [Required]
        public string NumberOfBook { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата создания")]
        public DateTime DateCreate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата удаления")]
        public DateTime? DateDelete { get; set; }

        [Display(Name = "Удален")]
        public bool IsDeleted { get; set; }

        public long? StudentGroupId { get; set; }

        [Display(Name = "Имя")]
        [MaxLength(20)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [MaxLength(30)]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        [MaxLength(30)]
        public string Patronymic { get; set; }

		[Display(Name = "Почта")]
		[MaxLength(150)]
		[Required]
		public string Email { get; set; }

		public StudentState StudentState { get; set; }

        [Display(Name = "О себе")]
        public string Description { get; set; }

        [Display(Name = "Фото")]
        public byte[] Photo { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }

        [ForeignKey("StudentId")]
        public virtual List<StudentHistory> StudentHistory { get; set; }

		[ForeignKey("StudentId")]
		public virtual List<DisciplineStudentRecord> DisciplineStudentRecords { get; set; }

		[ForeignKey("StudentId")]
		public virtual List<DisciplineLessonStudentRecord> DisciplineLessonStudentRecords { get; set; }

		[ForeignKey("StudentId")]
		public virtual List<DisciplineLessonTaskStudentRecord> DisciplineLessonTaskStudentRecords { get; set; }
	}
}
