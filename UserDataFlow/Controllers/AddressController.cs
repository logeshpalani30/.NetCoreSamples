using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserDataFlow.Interface;
using UserDataFlow.Model.Address;

namespace UserDataFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IAddress _address;

        public AddressController(ILogger<AddressController> logger, IAddress address)
        {
            _logger = logger;
            _address = address;
        }

        [HttpGet("getUserAddress/{userId}/contactId/{contactId}")]
        public IActionResult Get(int userId, int contactId)
        {
            try
            {
                var address = _address.GetAddress(userId, contactId);
                return new OkObjectResult(address);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpGet("getUserAllAddress/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var address = _address.GetAddress(id);
                return new OkObjectResult(address);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new BadRequestObjectResult(e.Message);
            }
        }

        // POST api/<AddressController>
        [HttpPost("addAddress")]
        public IActionResult Post([FromBody] AddressReq addressReq)
        {
            try
            {

                return new OkObjectResult("");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new BadRequestObjectResult(e.Message);
            }
        }

        // PUT api/<AddressController>/5
        [HttpPut("updateAddress/{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("deleteUser/userId/{userId}/contactId/{contactId}")]
        public IActionResult Delete(int userId,int contactId)
        {
            try
            {
                _address.DeleteAddress(userId, contactId);
                return new OkObjectResult("Address deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
