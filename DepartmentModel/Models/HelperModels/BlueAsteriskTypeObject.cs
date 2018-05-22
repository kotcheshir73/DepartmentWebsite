namespace DepartmentModel.Models.HelperModels
{
    /// <summary>
    /// Вспомогательный класс для загрузки учебных планов по синей звездочке
    /// Фиксирует типы дисципилин (базовая, выборочная или альтернативная)
    /// и определяет, включать нагрузку в расчет штатов или нет
    /// </summary>
    public class BlueAsteriskTypeObject
    {
        public string TypeName { get; set; }

        public string Code { get; set; }

        public bool IncludeInCalc { get; set; }
    }
}
