using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.HandleResponses
{
    public class ValidationErrorRespose : CustomException
    {
        public ValidationErrorRespose() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}
