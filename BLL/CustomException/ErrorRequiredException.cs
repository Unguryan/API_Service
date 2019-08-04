using System;

namespace BLL.CustomException
{
    class ErrorRequiredException : Exception
    {
        public ErrorRequiredException(string message) : base(message)
        {
        }
    }
}
