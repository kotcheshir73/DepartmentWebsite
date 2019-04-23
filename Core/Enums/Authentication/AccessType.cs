namespace Enums
{
    public enum AccessType : int
	{
		/// <summary>
		/// Только просмотр
		/// </summary>
		View = 1,

		/// <summary>
		/// Добавление/Изменение
		/// </summary>
		Change = 2,

		/// <summary>
		/// Удаление
		/// </summary>
		Delete = 4,

		/// <summary>
		/// Доступ к адмике
		/// </summary>
		Administrator = 8
	}
}