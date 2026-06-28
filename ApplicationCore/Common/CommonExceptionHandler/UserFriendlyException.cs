using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonExceptionHandler
{
    public class UserFriendlyException : Exception
    {
        public UserFriendlyException(string message, bool isShowFromInterceptor = true) : base(message)
        { }  
    }    
    
    public class UserFriendlyExceptionForUI : Exception
    {
        public UserFriendlyExceptionForUI(string message) : base(message)
        { }  
    }
}
