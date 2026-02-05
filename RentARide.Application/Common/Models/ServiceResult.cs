using System;
using System.Collections.Generic;

namespace RentARide.Application.Common.Models
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; }
        public T? Data { get; }
        public string Error { get; }
        public List<string> ValidationErrors { get; }

        private ServiceResult(bool isSuccess, T? data, string error, List<string> validationErrors)
        {
            IsSuccess = isSuccess;
            Data = data;
            Error = error;
            ValidationErrors = validationErrors;
        }

        public static ServiceResult<T> Success(T data)
        {
            return new ServiceResult<T>(true, data, string.Empty, null);
        }

        public static ServiceResult<T> Failure(string error)
        {
            return new ServiceResult<T>(false, default, error, null);
        }

         public static ServiceResult<T> Failure(List<string> validationErrors)
        {
            return new ServiceResult<T>(false, default, "Validation failed", validationErrors);
        }
    }
}
