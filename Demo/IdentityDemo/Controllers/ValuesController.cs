using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQ.Foundation.AspNetCore.Filters;
using Microsoft.AspNetCore.Mvc;

namespace IdentityDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUnduplicateService _unduplicateService;
        private readonly IManService _personService;

        private static List<IUnduplicateService> Service;

        public ValuesController(IUnduplicateService unduplicateService, IManService personService)
        {
            _unduplicateService = unduplicateService;
            _personService = personService;

            if (Service == null)
            {
                Service = new List<IUnduplicateService>();
            }
        }

        // GET api/values
        [HttpGet]
        //[Unduplicate]
        public ActionResult<IEnumerable<string>> Get()
        {
            //Service.Add(_unduplicateService);

            //if (Service.Count > 1)
            //{
            //    string[] a = { (Service[0].Equals(Service[1])).ToString() };
            //    Service.RemoveAt(0);

            //    return a;
            //}

            _personService.GetName();
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Unduplicate]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [Unduplicate]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public interface IPersonService
    {
        void GetName();
    }

    public interface IManService : IPersonService { }

    public class ManService : IManService
    {
        public void GetName()
        {
            Console.WriteLine("James");
        }
    }
}
