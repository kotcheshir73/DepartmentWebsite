namespace Models.Enums
{
	public enum ResultServiceStatusCode
	{
		/// <summary>
		/// Успешно
		/// </summary>
		Success = 200,
		/// <summary>
		/// Ошибка общая
		/// </summary>
		Error = 400,
		/// <summary>
		/// Элемент уже сущствует
		/// </summary>
		ExsistItem = 401,
		/// <summary>
		/// Не найдено
		/// </summary>
		NotFound = 404,
		/// <summary>
		/// Не найден файл
		/// </summary>
		FileNotFound = 405
	}
}