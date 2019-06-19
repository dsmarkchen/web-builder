using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    public class ModuleController : Controller
    {
        private readonly NHibernate.ISession _session;
        public ModuleController(NHibernate.ISession session)
            : base()
        {
            _session = session;
         
        }


        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<ModuleDto> Get()
        {
            var xs = _session.Query<Module>();
            IList<ModuleDto> lst = new List<ModuleDto>();
            foreach(Module node in xs)
            {
                var dto = new ModuleDto
                {
                    Id = node.Id,
                    Name = node.Name
                };

                foreach (Record r in node.Records)
                {
                    RecordDto rDto = new RecordDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Content = r.Content,
                    };

                    dto.Records.Add(rDto);
                    dto.RecordsCount++;
                }
                lst.Add(dto);
            }
            return lst;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ModuleDto Get(int id)
        {
            using (var transaction = _session.BeginTransaction())
            {
                var node = _session.Get<Module>(id);
                if (node == null) return null;

                var dto = new ModuleDto
                {
                    Name = node.Name,
                    Id = node.Id,

                };
                foreach (Record r in node.Records)
                {
                    RecordDto rDto = new RecordDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Content = r.Content,
                    };

                    dto.Records.Add(rDto);
                }
                return dto;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }


  
}
