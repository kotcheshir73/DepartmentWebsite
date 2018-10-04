using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentWebsite
{
    /// <summary>
    /// Представляет различные виды выделения дат в MonthView
    /// </summary>
    public enum MonthViewSelection
    {
        /// <summary>
        /// Пользователь может выбрать любую доступную дату мышью
        /// </summary>
        Manual,

        /// <summary>
        /// Выбор ограничен только одним днем
        /// </summary>
        Day,

        /// <summary>
        /// Выбор ограничен неделей (без выходных)
        /// </summary>
        WorkWeek,

        /// <summary>
        /// Выбор ограничен полной неделей
        /// </summary>
        Week,

        /// <summary>
        /// Выбор ограничен месяцем
        /// </summary>
        Month
    }
}
