using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.HandleResponses
{
    public class CustomException : Response
    {
        public CustomException(int statusCode, string? message=null,string? detials=null) : base(statusCode, message)
        {
        }

        public string?Details { get; set; }
    }
}
