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

        [HttpPost("addAddress")]
        public IActionResult Post([FromBody] AddressReq addressReq)
        {
            try
            {
                var address = _address.AddAddress( addressReq);
                return new OkObjectResult(address);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpPut("updateAddress")]
        public IActionResult Put([FromBody] AddressRes req)
        {
            try
            {
                var res = _address.UpdateAddress(req);
                return new OkObjectResult(res);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpDelete("deleteUser/userId/{userId}/addressId/{addressId}")]
        public IActionResult Delete(int userId,int addressId)
        {
            try
            {
                _address.DeleteAddress(userId, addressId);
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
