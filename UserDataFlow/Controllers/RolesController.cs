using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UserDataFlow.Interface;
using UserDataFlow.Model.Role;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserDataFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IRoles _rolesAndDepartment;

        public RolesController(ILogger<RolesController> logger,IRoles rolesAndDepartment  )
        {
            _logger = logger;
            _rolesAndDepartment = rolesAndDepartment;
        }
        // GET: api/<RolesController>
        [HttpGet("getDepartmentAndRoles")]
        public IActionResult Get()
        {
            try
            {
                var departmentsAndRoles = _rolesAndDepartment.GetRoleAndDepartments();
                return new OkObjectResult(departmentsAndRoles);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        // GET api/<RolesController>/5
        [HttpGet("{id}")]
        internal string Get(int id)
        {
            return "value";
        }

        // POST api/<RolesController>
        [HttpPost("addRole")]
        public IActionResult Post([FromBody] RoleReq req)
        {
            try
            {
                var role = _rolesAndDepartment.AddRole(req);
                return new OkObjectResult(role);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("addDepartment")]
        public IActionResult Post([FromBody] DepartmentReq req)
        {
            try
            {
                var dept = _rolesAndDepartment.AddDepartment(req);
                return new OkObjectResult(dept);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        internal void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        internal void Delete(int id)
        {
        }
    }
}
