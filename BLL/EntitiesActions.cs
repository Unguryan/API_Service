using BLL.Models;
using DAL;
using DAL.Models;

using System.Collections.Generic;
using BLL.CustomException;

namespace BLL
{
    public class EntitiesActions
    {
        private readonly UnitOfWork uow;
        TypesActions TA = new TypesActions();

        public EntitiesActions(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public EntitiesActions()
        {

            APIContext context = new APIContext();
            uow = new UnitOfWork(context, new ContextRepository<AttributeDAL>(context), new ContextRepository<EntityDAL>(context), new ContextRepository<TypeDAL>(context),
                new ContextRepository<StringAttribute>(context), new ContextRepository<StringRequired>(context));
        }


        public virtual List<EntityBLL> GetEntities()
        {
            List<EntityBLL> List = new List<EntityBLL>();
            foreach (var t in uow.Entities.Get())
            {
                Dictionary<string, object> Dict = new Dictionary<string, object>();
                foreach (var g in t.Attributes)
                {
                    Dict.Add(g.Key, g.Value.Item );
                }
                List.Add(new EntityBLL() { Id = t.Id, Attributes = Dict, NameEntity = t.NameEntity });
            }
            return List;
        }

        public virtual List<EntityBLL> GetEntitiesByName(string name)
        {
            List<EntityBLL> List = new List<EntityBLL>();
            foreach (var t in uow.Entities.Get(o => o.NameEntity == name))
            {
                Dictionary<string, object> Dict = new Dictionary<string, object>();
                foreach (var g in t.Attributes)
                {
                    Dict.Add(g.Key, g.Value.Item );
                }
                List.Add(new EntityBLL() { Id = t.Id, Attributes = Dict, NameEntity = t.NameEntity });
            }
            return List;
        }

        public virtual EntityBLL GetEntityById(int id)
        {
            EntityBLL entity = new EntityBLL();
            EntityDAL entityDAL = uow.Entities.GetOne(o => o.Id == id);
            if (entityDAL != null)
            {
                Dictionary<string, object> Dict = new Dictionary<string, object>();
                foreach (var g in entityDAL.Attributes)
                {
                    Dict.Add(g.Key, g.Value.Item );
                }

                entity.Id = entityDAL.Id;
                entity.NameEntity = entityDAL.NameEntity;
                entity.Attributes = Dict;
                return entity;
            }
            else
            {
                throw new EntityNotFoundByIDException(string.Format("The entity under this (\"{0}\") ID was not found.", id));
            }
        }

        public virtual bool AddEntity(EntityBLL New)
        {

            uow.Entities.Create(Check(New));
            uow.Save();
            return true;

        }

        public virtual bool DeleteEntityByID(int id)
        {
            uow.Entities.Remove(uow.Entities.GetOne(x => x.Id == id));
            uow.Save();
            return true;
        }


        public virtual bool ChangeEntity(EntityBLL New)
        {
            if (uow.Entities.GetOne(o => o.Id == New.Id) != null)
            {
                EntityDAL type = Check(New);
                uow.Entities.Remove(uow.Entities.GetOne(o => o.Id == New.Id));
                uow.Save();
                uow.Entities.Create(type);
                uow.Save();
                return true;
            }
            else
            {
                throw new EntityNotFoundByIDException(string.Format("Entity with this \"{0}\" ID not Found.", New.Id));
            }
            
        }

        private EntityDAL Check(EntityBLL New)
        {
            TypeBLL type = TA.GetTypeBLLByName(New.NameEntity.ToLower());
            if (type != null)
            {
                if (type.Required != null)
                {
                    foreach (var t in type.Required)
                    {
                        object attribute = new object();
                        New.Attributes.TryGetValue(t, out attribute);
                        if (attribute == null || attribute.ToString() == "")
                            throw new ErrorRequiredException(string.Format("Required field \"{0}\" is empty.", t));
                    }
                }

                if (type.MaxLength.Count != 0)
                {
                    foreach (var t in type.MaxLength)
                    {
                        bool check = New.Attributes[t.Key].ToString().Length > int.Parse(type.MaxLength[t.Key]);
                        if (check == true) throw new OutOfMaxLengthException(string.Format("The field \"{0}\" accepts no more than {1} characters.", t, int.Parse(type.MaxLength[t.Key])));
                    }
                }
                if (type.MinLength.Count != 0)
                {
                    foreach (var t in type.MinLength)
                    {
                            bool check = New.Attributes[t.Key].ToString().Length < int.Parse(type.MinLength[t.Key]);
                        if (check == true) throw new OutOfMinLengthException(string.Format("The field \"{0}\" accepts no less than {1} characters.", t, int.Parse(type.MinLength[t.Key])));
                    }
                }
                if (type.Attributes != null)
                {
                    Dictionary<string, AttributeDAL> AttributesDal = new Dictionary<string, AttributeDAL>();

                    foreach (var t in type.Attributes)
                    {
                        if (New.Attributes.ContainsKey(t))
                            AttributesDal.Add(t.ToLower(), new AttributeDAL() { Item = New.Attributes[t] });
                    }

                    return new EntityDAL { NameEntity = New.NameEntity, Attributes = AttributesDal };
                }
                else
                {
                    throw new AttributesNotFoundException("Attributes Not Found.");
                }

            }
            else
            {
                throw new TypeNotFoundException(string.Format("Type \"{0}\" not found", New.NameEntity.ToLower()));
            }
        }
    }
}
