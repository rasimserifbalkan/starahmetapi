using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarAhmet.ResponseModels
{
    public class ResponseBaseModel<T>
    {
        public string ErrorCode { get; set; }
        public T Data { get; set; }
    }
}
