using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHrambaTest.Models
{
    public class ErrorDto
    {
        public string message { get; set; }
        public int statusCode { get; set; }
        public string exceptionClass { get; set; }
        public IEnumerable<ErrorStack> stack {get; set;}
        public IEnumerable<ErrorSuppresed> suppressed { get; set; }

        
    }

    public class ErrorStack
    {
        public string declaringClass { get; set; }
        public string methodName { get; set; }  
        public string fileName { get; set; }
        public int lineNumber { get; set; }
    }

    public class ErrorSuppresed
    {
        public string cause { get; set; }
        public IEnumerable<ErrorStackTrace> stackTrace { get; set; }
        public string message { get; set; }
        public string localizedMessage { get; set; }
        public IEnumerable<string> suppressed { get; set; }
    }

    public class ErrorStackTrace
    {
        public string methodName { get; set; }
        public string fileName { get; set; }
        public int lineNumber { get; set; }
        public string className { get; set; }
        public bool nativeMethod { get; set; }
    }

}
