using System;

namespace BLL.CustomException
{
    class TypeNotFoundException : Exception
    {
        public TypeNotFoundException(string message) : base(message)
        {
        }
    }
}
