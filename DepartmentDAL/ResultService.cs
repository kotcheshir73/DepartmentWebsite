using DepartmentDAL.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace DepartmentDAL
{
	public class ResultService
	{
		public bool Succeeded { get; private set; }

		public ResultServiceStatusCode StatusCode { get; private set; }

		public Dictionary<string, string> Errors { get; private set; }

		public ResultService()
		{
			Errors = new Dictionary<string, string>();
			Succeeded = true;
			StatusCode = 0;
		}

		public void AddError(string key, string value)
		{
			Errors.Add(key, value);
			if (Succeeded)
				Succeeded = false;
		}

		public static ResultService Error(string key, string error, ResultServiceStatusCode statusCode)
		{
			var result = new ResultService();

			result.Succeeded = false;
			result.Errors.Add(key, error);
			result.StatusCode = statusCode;

			return result;
		}

		public static ResultService Error(Exception error, ResultServiceStatusCode statusCode)
		{
			var result = new ResultService();

			result.Succeeded = false;
			result.Errors.Add("Error:", error.Message);
			while (error.InnerException != null)
			{
				error = error.InnerException;
				result.Errors.Add("Inner error:", error.Message);
			}
			result.StatusCode = statusCode;

			return result;
		}

		public static ResultService Error(DbEntityValidationException error, ResultServiceStatusCode statusCode)
		{
			var result = new ResultService();

			result.Succeeded = false;
			result.Errors.Add("DbEntityValidation Error:", error.Message);
			foreach (var eve in error.EntityValidationErrors)
			{
				foreach (var ve in eve.ValidationErrors)
				{
					result.Errors.Add("ValidationErrors:", string.Format("- Entity: \"{0}\", Error: \"{1}\"\r\n",
						ve.PropertyName, ve.ErrorMessage));
				}
			}
			result.StatusCode = statusCode;

			return result;
		}

		public static ResultService Success()
		{
			return new ResultService
			{
				Succeeded = true,
				StatusCode = ResultServiceStatusCode.Success
			};
		}
	}

	public class ResultService<T>
	{
		public bool Succeeded { get; private set; }

		public ResultServiceStatusCode StatusCode { get; private set; }

		public Dictionary<string, string> Errors { get; private set; }

		public List<T> List { get; private set; }

		public T Result { get; private set; }

		public ResultService()
		{
			Errors = new Dictionary<string, string>();
			Succeeded = true;
			StatusCode = 0;
		}

		public void AddError(string key, string value)
		{
			Errors.Add(key, value);
			if (Succeeded)
				Succeeded = false;
		}

		public static ResultService<T> Error(string key, string error, ResultServiceStatusCode statusCode)
		{
			var result = new ResultService<T>();

			result.Succeeded = false;
			result.Errors.Add(key, error);
			result.StatusCode = statusCode;

			return result;
		}

		public static ResultService<T> Error(Exception error, ResultServiceStatusCode statusCode)
		{
			var result = new ResultService<T>();

			result.Succeeded = false;
			result.Errors.Add("Error:", error.Message);
			while(error.InnerException != null)
			{
				error = error.InnerException;
				result.Errors.Add("Inner error:", error.Message);
			}
			result.StatusCode = statusCode;

			return result;
		}

		public static ResultService<T> Error(DbEntityValidationException error, ResultServiceStatusCode statusCode)
		{
			var result = new ResultService<T>();

			result.Succeeded = false;
			result.Errors.Add("DbEntityValidation Error:", error.Message);
			foreach (var eve in error.EntityValidationErrors)
			{
				foreach (var ve in eve.ValidationErrors)
				{
					result.Errors.Add("ValidationErrors:", string.Format("- Entity: \"{0}\", Error: \"{1}\"\r\n",
						ve.PropertyName, ve.ErrorMessage));
				}
			}
			result.StatusCode = statusCode;

			return result;
		}

		public static ResultService<T> Success(T result)
		{
			return new ResultService<T>
			{
				Succeeded = true,
				StatusCode = ResultServiceStatusCode.Success,
				Result = result
			};
		}
	}
}
