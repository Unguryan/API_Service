using System;

namespace BLL.CustomException
{
    public class EntityNotFoundByIDException : Exception
    {
        public EntityNotFoundByIDException(string message) : base(message)
        {
        }
    }
}
