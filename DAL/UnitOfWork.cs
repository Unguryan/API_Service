using DAL.Models;
using System;

namespace DAL
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        public APIContext BD;

        public ContextRepository<AttributeDAL> Attributes { get; }

        public ContextRepository<EntityDAL> Entities { get; }

        public ContextRepository<TypeDAL> Types { get; }

        public ContextRepository<StringAttribute> StrAttribure { get; }

        public ContextRepository<StringRequired> StringRequired { get; }

        private bool Disposed;

        public UnitOfWork(APIContext bD, ContextRepository<AttributeDAL> attributes, ContextRepository<EntityDAL> entities, ContextRepository<TypeDAL> types, ContextRepository<StringAttribute> strAttribure, ContextRepository<StringRequired> stringRequired)
        {
            BD = bD;
            Attributes = attributes;
            Entities = entities;
            Types = types;
            StrAttribure = strAttribure;
            StringRequired = stringRequired;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed) return;
            if (disposing)
            {
                BD.Dispose();
            }

            Disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            BD.SaveChanges();
        }
    }
}
