namespace DepartmentModel.Models.HelperModels
{
    /// <summary>
    /// Вспомогательный класс для загрузки учебных планов по синей звездочке
    /// Фиксирует к какому циклу относится дисциплина
    /// На данный момент нужна для отсеивания факультативов
    /// </summary>
    public class BlueAsteriskBlockType
    {
        public string BlockName { get; set; }

        public string Identificator { get; set; }

        public string Code { get; set; }

        public bool IsFacultative { get; set; }
    }
}
