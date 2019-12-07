namespace AcademicYearInterfaces.HelperModels
{
    /// <summary>
    /// Вспомогательный класс для загрузки учебных планов по синей звездочке
    /// Фиксирует вид дисципилин (базовая, выборочная или альтернативная)
    /// и определяет, включать нагрузку в расчет штатов или нет
    /// </summary>
    public class BlueAsteriskDisicplineType
    {
        public string TypeName { get; set; }

        public string Code { get; set; }
    }
}