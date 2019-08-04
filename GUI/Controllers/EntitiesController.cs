using System.Collections.Generic;
using BLL;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace TestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntitiesController : ControllerBase
    {
        EntitiesActions EA = new EntitiesActions();
        // GET: api/Entities
        [HttpGet]
        public IEnumerable<EntityBLL> Get()
        {
            return EA.GetEntities();
        }

        // GET: api/Entities/name
        [HttpGet("{NameEntity}", Name = "GetEntities")]
        public IEnumerable<EntityBLL> Get(string NameEntity)
        {
            return EA.GetEntitiesByName(NameEntity);
        }


        // POST: api/Entities
        [HttpPost("{Name}", Name ="PostEntity")]
        public void Post([FromBody] EntityBLL value, string Name)
        {
            value.NameEntity = Name;
            EA.AddEntity(value);
        }

        // PUT: api/Entities/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] EntityBLL value)
        {
            value.Id = id;
            EA.ChangeEntity(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            EA.DeleteEntityByID(id);
        }
    }
}
