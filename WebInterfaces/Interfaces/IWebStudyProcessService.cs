using System;
using System.Collections.Generic;

namespace WebInterfaces.Interfaces
{
    public interface IStudyProcessService
    {
        /// <summary>
        /// Получение отображаемых имен свойств типа и их названий
        /// </summary>
        /// <param name="type">Тип</param>
        /// <returns></returns>
        (List<string> displayNames, List<string> propertiesNames) GetPropertiesNames(Type type);

        /// <summary>
        /// Получение значений переданных свойств каждого элемента списка
        /// </summary>
        /// <param name="list">Список объектов</param>
        /// <param name="propertiesNames">Список с названиями свойств</param>
        /// <returns></returns>
        List<List<object>> GetPropertiesValues<T>(List<T> list, List<string> propertiesNames);
    }
}
