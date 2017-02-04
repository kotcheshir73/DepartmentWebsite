using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentDAL
{
    public class ResultService
    {
        public bool Succeeded { get; private set; }

        public int StatusCode { get; private set; }

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

        public static ResultService Error(string key, string error, int statusCode)
        {
            var result = new ResultService();

            result.Succeeded = false;
            result.Errors.Add(key, error);
            result.StatusCode = statusCode;

            return result;
        }

        public static ResultService Success()
        {
            return new ResultService { Succeeded = true };
        }

        public static ResultService InternalError()
        {
            var result = new ResultService();

            result.AddError("server", "internal");

            return result;
        }
    }
}
