using System;

namespace BLL.CustomException
{
    class OutOfMaxLengthException : Exception
    {
        public OutOfMaxLengthException(string message) : base(message)
        {
        }
    }
}
