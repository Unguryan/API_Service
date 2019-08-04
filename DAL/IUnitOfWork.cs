using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public interface IUnitOfWork
    {
        ContextRepository<AttributeDAL> Attributes { get; }

        ContextRepository<EntityDAL> Entities { get; }

        ContextRepository<TypeDAL> Types { get; }

        ContextRepository<StringAttribute> StrAttribure { get; }

        ContextRepository<StringRequired> StringRequired { get; }

        void Save();
        void Dispose();


    }


}
