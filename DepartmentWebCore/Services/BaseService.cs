using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using BaseInterfaces.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentWebCore.Services
{
	public class BaseService
	{
		private readonly ILecturerService _lecturerService;

		private IMemoryCache cache;

		public BaseService(ILecturerService lecturerService, IMemoryCache memoryCache)
		{
			_lecturerService = lecturerService;

			cache = memoryCache;
		}

		public List<LecturerViewModel> GetLecturers()
		{
			var lecturers = _lecturerService.GetLecturers(new LecturerGetBindingModel { SkipCheck = true });
			if(!lecturers.Succeeded)
			{
				return null;
			}

			var orderList = new List<string>
					{
						"ЗаведующийКафедрой",
						"ЗаместительЗаведующегоКафедрой"
					};

			var result = new List<LecturerViewModel>();

			//TODO добавить order к LecturerPost
			foreach (var item in orderList)
			{
				var tmp = lecturers.Result.List.FirstOrDefault(x => x.Post == item);
				result.Add(tmp);
				lecturers.Result.List.Remove(tmp);
			}
			foreach (var item in lecturers.Result.List)
			{
				result.Add(item);
			}

			return result;
		}
	}
}
