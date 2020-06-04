using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPIDemo.Models;
using WebAPIDemo.Utils;

namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetTeamMemberController : ControllerBase
    {
        // GET: api/GetTeamMember
        [HttpGet]
        public IEnumerable<TeamMember> Get()
        {
            var jsonData = JsonConvert.SerializeObject(Data.GetStaticMembers());

            //System.IO.File.WriteAllText(@"Utils/", jsonData);
            //var path =@"Utils/254.json";

            return Data.GetStaticMembers();
        }

        // GET: api/GetTeamMember/5
        [HttpGet("{id}", Name = "Get")]
        public TeamMember Get(int id)
        {
            var member = Data.GetStaticMembers().Where(x => x.Id == id).FirstOrDefault();

            return member!=null?member :new TeamMember();
        }

        // POST: api/GetTeamMember
        [HttpPost]
        public string Post([FromBody] TeamMember member)
        {
            var success = Data.AddData(member);

            return success? "Successfully Added":"Failed to add data";
        }

        // PUT: api/GetTeamMember/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
