using System;

namespace BLL.CustomException
{
    class OutOfMinLengthException : Exception
    {
        public OutOfMinLengthException(string message) : base(message)
        {
        }
    }
}
