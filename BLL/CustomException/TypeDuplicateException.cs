using System;

namespace BLL.CustomException
{
    class TypeDuplicateException : Exception
    {
        public TypeDuplicateException(string message) : base(message)
        {
        }
    }
}
