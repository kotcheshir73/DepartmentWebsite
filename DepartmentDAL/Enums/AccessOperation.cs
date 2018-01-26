﻿namespace DepartmentDAL.Enums
{
	public enum AccessOperation
	{
		// Меню Администрирование
		Администрирование = 0,

		Роли = 1,

		Доступы = 2,

		Пользователи = 3,

		// Меню Учебный процесс
		Учебный_процесс = 100,

		Направления = 101,

		Преподаватели = 102,

		Дисциплины = 103, // + Блоки дисциплин

		Группы = 104, // + Потоки

		Аудитории = 105,

		Даты_семестра = 106,

		Студенты = 110,

		Студенты_учащиеся = 111,

		Студенты_завершившие = 112,

		Студенты_академики = 113,

		Студенты_отчисленные = 114,

		Учебные_планы = 120,

		Виды_нагрузок = 121,

		Учебные_года = 122,

		Контингент = 123,

		Нормы_времени = 124,

		Расчет_штатов = 125,

        Должности_преподавателей = 126,

        // Меню Расписание
        Расписание = 200,

		Расписание_аудитории = 201,

		Расписание_группы = 202,

		Расписание_преподаватели = 203,

		Расписание_настройки = 204,

		Расписание_интервалы_пар = 205,

        Расписание_дисциплины = 206,

        // Меню Сервис
        Сервис = 300,

		Генерация_билетов = 301
	}
}
