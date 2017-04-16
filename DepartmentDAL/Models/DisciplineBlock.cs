using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentDAL.Models
{
	/// <summary>
	/// Класс, описыывающий блоки дисциплин, на которые разбиваются все дисциплины в расчете штатов
	/// </summary>
	public class DisciplineBlock : BaseEntity
	{
		public string Title { get; set; }
		
		[ForeignKey("DisciplineBlockId")]
		public virtual List<Discipline> Disciplines { get; set; }
	}
}
