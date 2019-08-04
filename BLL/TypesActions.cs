using BLL.Models;
using DAL;
using DAL.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BLL.CustomException;
using System.Linq;
using System;

namespace BLL
{
    public class TypesActions
    {
        private readonly UnitOfWork uow;

        public TypesActions(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public TypesActions()
        {
            APIContext context = new APIContext();
            uow = new UnitOfWork(context, new ContextRepository<AttributeDAL>(context), new ContextRepository<EntityDAL>(context), new ContextRepository<TypeDAL>(context),
                new ContextRepository<StringAttribute>(context), new ContextRepository<StringRequired>(context));
        }


        public virtual List<TypeBLL> GetTypes()
        {
            List <TypeBLL> List = new List<TypeBLL>();
            foreach(var t in uow.Types.Get())
            {
                t.Attributes = uow.StrAttribure.Get(o => o.TypeDALId == t.Id);
                t.Required = uow.StringRequired.Get(o => o.TypeDALId == t.Id);
                List<string> Att = new List<string>();
                List<string> Req = new List<string>();
                foreach(var g in t.Attributes)
                {
                    Att.Add(g.Attribute);
                }
                foreach(var g in t.Required)
                {
                    Req.Add(g.Required);
                }
                List.Add(new TypeBLL() { NameType = t.NameType, Attributes = Att, Required = Req, MaxLength = t.MaxLength, MinLength = t.MinLength, Id = t.Id});
            }
            return List;
        }

        public virtual TypeBLL GetTypeBLLByName(string name)
        {
            TypeBLL type = new TypeBLL();
            TypeDAL typeDal = uow.Types.GetOne(o => o.NameType == name);
            if(typeDal!= null)
            {
                type.Id = typeDal.Id;
                type.MaxLength = typeDal.MaxLength;
                type.MinLength = typeDal.MinLength;

                typeDal.Attributes = uow.StrAttribure.Get(o => o.TypeDALId == typeDal.Id);
                typeDal.Required = uow.StringRequired.Get(o => o.TypeDALId == typeDal.Id);
                List<string> Att = new List<string>();
                List<string> Req = new List<string>();
                foreach (var g in typeDal.Attributes)
                {
                    Att.Add(g.Attribute);
                }
                foreach (var g in typeDal.Required)
                {
                    Req.Add(g.Required);
                }
                type.Required = Req;
                type.Attributes = Att;
                return type;
            }
            else
            {
                throw new TypeNotFoundException(string.Format("Type \"{0}\" not found." ,name));
            }
            
        }

        public virtual bool AddType(TypeBLL New)
        {
            if(uow.Types.GetOne(x => (x.NameType.ToLower() == New.NameType.ToLower())) == null)
            { 
                
                uow.Types.Create(Check(New));
            uow.Save();
            return true;
            }
            else
            {
                throw new TypeDuplicateException(string.Format("Type \"{0}\" already exist.", New.NameType.ToLower()));
            }
        }

        public virtual bool DeleteTypeByName(int id)
        {
            uow.Types.Remove(uow.Types.GetOne(x => x.Id == id));
            uow.Save();
            return true;
        }


        public virtual bool ChangeType(TypeBLL New)
        {
            if(uow.Types.GetOne(o => o.Id == New.Id) != null) { 
                TypeDAL type = Check(New);
                uow.Types.Remove(uow.Types.GetOne(o => o.Id == New.Id));
                uow.Save();
                uow.Types.Create(type);
                uow.Save();
                return true;
            }
            else
            {
                throw new TypeNotFoundException(string.Format("Type with this \"{0}\" ID not Found.", New.Id));
            }
        }

        private TypeDAL Check(TypeBLL value)
        {
            TypeDAL type = new TypeDAL();
            List<StringRequired> Req = new List<StringRequired>();
            List<StringAttribute> Att = new List<StringAttribute>();
            if (value.Required != null)
            {
                foreach (var t in value.Required)
                {
                    if (!value.Attributes.Contains(t)) throw new IncorrectTypingException(string.Format("Attribute \"{0}\" not found", t));

                    Req.Add(new StringRequired() { Required = t, TypeDALId = value.Id });
                }
            }
            if (value.Attributes != null)
            {
                foreach (var t in value.Attributes)
                {
                    Att.Add(new StringAttribute() { Attribute = t, TypeDALId = value.Id });
                }
            }
            if(value.MaxLength.Count != 0)
            {
               foreach(var t in value.MaxLength)
                {
                    if(!value.Attributes.Contains(t.Key)) throw new IncorrectTypingException(string.Format("Attribute \"{0}\" not found",t.Key));

                    if (value.MinLength.ContainsKey(t.Key))
                    {
                        if(int.Parse(value.MinLength[t.Key]) >= int.Parse(t.Value))
                            throw new IncorrectTypingException(string.Format("Min value > Max value. In this \"{0}\" attribute",t.Key));
                    }
                }
            }
            if (value.MinLength.Count != 0)
            {
                foreach (var t in value.MinLength)
                {
                    if (!value.Attributes.Contains(t.Key)) throw new IncorrectTypingException(string.Format("Attribute \"{0}\" not found", t.Key));
                }
            }
            type.Required = Req;
            type.NameType = value.NameType.ToLower();
            type.MaxLength = value.MaxLength;
            type.MinLength = value.MinLength;
            type.Attributes = Att;
            return type;
            
        }
    }
}
