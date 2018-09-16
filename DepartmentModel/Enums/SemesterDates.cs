using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentProcessAccountingModel.Enums
{
    /// <summary>
    /// Даты семестра
    /// </summary>
    public enum SemesterDates
    {
        /// <summary>
        /// Дата начала первого полупериода (она же дата начала семестра)
        /// </summary>
        НПП = 0,
        /// <summary>
        /// Дата окончания первого полупериода
        /// </summary>
        ОПП = 1,
        /// <summary>
        /// Дата начала второго полупериода
        /// </summary>
        НВП = 2,
        /// <summary>
        /// Дата окончания второго полупериода (она же дата окончания семестра)
        /// </summary>
        ОВП = 3,
        /// <summary>
        /// Дата начала зачетов
        /// </summary>
        НЗ = 4,
        /// <summary>
        /// Дата окончания зачетов
        /// </summary>
        ОЗ = 5,
        /// <summary>
        /// Дата начала экзаменов
        /// </summary>
        НЭ = 6,
        /// <summary>
        /// Дата окончания экзаменов
        /// </summary>
        ОЭ = 7,
        /// <summary>
        /// Дата начала практики
        /// </summary>
        НП = 8,
        /// <summary>
        /// Дата окончания практики
        /// </summary>
        ОП = 9
    }
}
