﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentDAL.Models
{
	/// <summary>
	/// Класс, хранящий инорфмацио по контингенту
	/// </summary>
	public class Contingent : BaseEntity
	{
		public long AcademicYearId { get; set; }

		public long StudentGroupId { get; set; }

		public int CountStudetns { get; set; }

		public int CountSubgroups { get; set; }

		public virtual AcademicYear AcademicYear { get; set; }

		public virtual StudentGroup StudentGroup { get; set; }

		[ForeignKey("ContingentId")]
		public virtual List<LoadDistributionRecord> LoadDistributionRecord { get; set; }
	}
}
