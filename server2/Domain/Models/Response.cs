using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Response<T> where T : class
    {
        public bool HasError { get; set; }
        public string Message { get; set; }
        public T Model { get; set; }

        public Response(T model, bool hasError, string message)
        {
            Model = model;
            HasError = hasError;
            Message = message;
        }
    }
}
