using System;

namespace BLL.CustomException
{
    class AttributesNotFoundException : Exception
    {
        public AttributesNotFoundException(string message) : base(message)
        {
        }
    }
}
