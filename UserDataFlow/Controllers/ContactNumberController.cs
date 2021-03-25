using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using UserDataFlow.Interface;
using UserDataFlow.Model.Contact;

namespace UserDataFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactNumberController : ControllerBase
    {
        private readonly ILogger<ContactNumberController> _logger;
        private readonly IContactNumber _contactNumber;

        public ContactNumberController(ILogger<ContactNumberController> logger, IContactNumber contactNumber)
        {
            _logger = logger;
            _contactNumber = contactNumber;
        }

        [HttpGet("getContact/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var contacts = _contactNumber.GetContacts(id);
                return new CreatedResult($"{id}",contacts);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("addContact")]
        public IActionResult Post([FromBody] AddContactModel contact)
        {
            try
            {
                Response.StatusCode = StatusCodes.Status200OK;

                var contactModel = _contactNumber.AddContact(contact);
                return new OkObjectResult(contactModel);
            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;

                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
 
        // // PUT api/<ContactNumberController>/5
        [HttpPut("updateContact")]
        public IActionResult Put([FromBody] ContactNumberModel contact)
        {
            try
            {
                var updatedContact = _contactNumber.UpdateContact(contact);
                return new OkObjectResult(updatedContact);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete("deleteContact/userId/{userId}/contactId/{contactId}")]
        public IActionResult Delete(int userId, int contactId)
        {
            try
            {
                _contactNumber.DeleteContact(userId, contactId);
                return new OkObjectResult("Contact deleted successfully");
            }
            catch (Exception e)
            {
               _logger.LogError(e.Message);
               return BadRequest(e.Message);
            }
        }
    }
}
