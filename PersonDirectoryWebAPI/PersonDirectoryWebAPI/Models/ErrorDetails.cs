using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace PersonDirectory.WebAPI.Models
{
    public class ErrorDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
