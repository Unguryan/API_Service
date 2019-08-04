using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BLL;
using BLL.Models;

namespace TestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        TypesActions TA = new TypesActions();
        // GET: api/Types
        [HttpGet]
        public IEnumerable<TypeBLL> Get()
        {
            return TA.GetTypes();
        }

        // GET: api/Types/Name
        [HttpGet("{Name}", Name = "GetType")]
        public TypeBLL Get(string Name)
        {
            return TA.GetTypeBLLByName(Name);
        }

        // POST: api/Types
        [HttpPost("{NameType}", Name = "PostType")]
        public void Post([FromBody] TypeBLL value, string NameType)
        {
            value.NameType = NameType;
            TA.AddType(value);
        }

        // PUT: api/Types/5S
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] TypeBLL value)
        {
            value.Id = id;
            TA.ChangeType(value);
        }

        // DELETE: api/ApiWithActions/name
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            TA.DeleteTypeByName(id);
        }
    }
}
