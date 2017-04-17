﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentDAL.Models
{
	/// <summary>
	/// Класс, хранящий информацию по нормам времени
	/// </summary>
	public class TimeNorm : BaseEntity
	{
		[Display(Name = "Норма времени")]
		[MaxLength(50)]
		[Required]
		public string Title { get; set; }

		public long KindOfLoadId { get; set; }

		public string Formula { get; set; }

		public decimal Hours { get; set; }

		public virtual KindOfLoad KindOfLoad { get; set; }

		[ForeignKey("TimeNormId")]
		public virtual List<LoadDistributionRecord> LoadDistributionRecord { get; set; }
	}
}
