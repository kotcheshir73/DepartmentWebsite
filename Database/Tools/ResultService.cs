using System;
using System.Collections.Generic;

namespace Tools
{
    public class ResultService
    {
        public bool Succeeded { get; private set; }

        public ResultServiceStatusCode StatusCode { get; private set; }

        public List<KeyValuePair<string, string>> Errors { get; private set; }

        /// <summary>
        /// Какой-то объект, получаемый по результатам операции (например, id)
        /// </summary>
        public object Result { get; private set; }

        public ResultService()
        {
            Errors = new List<KeyValuePair<string, string>>();
            Succeeded = true;
            StatusCode = 0;
        }

        public void AddError(string key, string value)
        {
            Errors.Add(new KeyValuePair<string, string>(key, value));
            if (Succeeded)
            {
                Succeeded = false;
            }
        }

        public void AddError(Exception error)
        {
            Errors.Add(new KeyValuePair<string, string>("Общая ошибка", $"{error.Message}: {error.StackTrace}"));
            if (Succeeded)
            {
                Succeeded = false;
            }
        }

        public static ResultService Error(string key, string error, ResultServiceStatusCode statusCode)
        {
            var result = new ResultService
            {
                Succeeded = false
            };
            result.Errors.Add(new KeyValuePair<string, string>(key, error));
            result.StatusCode = statusCode;

            return result;
        }

        public static ResultService Error(Exception error, ResultServiceStatusCode statusCode)
        {
            var result = new ResultService
            {
                Succeeded = false
            };
            result.Errors.Add(new KeyValuePair<string, string>("Error:", $"{error.Message}: {error.StackTrace}"));

            while (error.InnerException != null)
            {
                error = error.InnerException;
                result.Errors.Add(new KeyValuePair<string, string>("Inner error:", $"{error.Message}: {error.StackTrace}"));
            }
            result.StatusCode = statusCode;

            return result;
        }

        public static ResultService Error(string key, Exception error, ResultServiceStatusCode statusCode)
        {
            var result = new ResultService
            {
                Succeeded = false
            };
            result.Errors.Add(new KeyValuePair<string, string>(key, $"{error.Message}: {error.StackTrace}"));

            while (error.InnerException != null)
            {
                error = error.InnerException;
                result.Errors.Add(new KeyValuePair<string, string>("Inner error:", $"{error.Message}: {error.StackTrace}"));
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

        public static ResultService Success(object obj)
        {
            return new ResultService
            {
                Result = obj,
                Succeeded = true,
                StatusCode = ResultServiceStatusCode.Success
            };
        }
    }

    public class ResultService<T>
    {
        public bool Succeeded { get; private set; }

        public ResultServiceStatusCode StatusCode { get; private set; }

        public List<KeyValuePair<string, string>> Errors { get; private set; }

        public List<T> List { get; private set; }

        public T Result { get; private set; }

        public ResultService()
        {
            Errors = new List<KeyValuePair<string, string>>();
            Succeeded = true;
            StatusCode = 0;
        }

        public void AddError(string key, string value)
        {
            Errors.Add(new KeyValuePair<string, string>(key, value));
            if (Succeeded)
            {
                Succeeded = false;
            }
        }

        public static ResultService<T> Error(string key, string error, ResultServiceStatusCode statusCode)
        {
            var result = new ResultService<T>
            {
                Succeeded = false
            };
            result.Errors.Add(new KeyValuePair<string, string>(key, error));
            result.StatusCode = statusCode;

            return result;
        }

        public static ResultService<T> Error(Exception error, ResultServiceStatusCode statusCode)
        {
            var result = new ResultService<T>
            {
                Succeeded = false
            };
            result.Errors.Add(new KeyValuePair<string, string>("Error:", error.Message));
            while (error.InnerException != null)
            {
                error = error.InnerException;
                result.Errors.Add(new KeyValuePair<string, string>("Inner Error:", error.Message));
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
