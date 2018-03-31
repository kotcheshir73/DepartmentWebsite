﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DepartmentModel.Models
{
    /// <summary>
    /// Класс, описыывающий блоки дисциплин, на которые разбиваются все дисциплины в расчете штатов
    /// </summary>
    [DataContract]
    public class DisciplineBlock : BaseEntity
    {
        [Required]
        [DataMember]
        public string Title { get; set; }

        //-------------------------------------------------------------------------

        //-------------------------------------------------------------------------

        [ForeignKey("DisciplineBlockId")]
		public virtual List<Discipline> Disciplines { get; set; }
	}
}