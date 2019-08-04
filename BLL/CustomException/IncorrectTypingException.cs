using System;

namespace BLL.CustomException
{
    public class IncorrectTypingException : Exception
    {
        public IncorrectTypingException(string message) : base(message)
        {
        }
    }
}
