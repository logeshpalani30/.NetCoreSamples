using EfAdoTemplateTest.Controllers;
using EfAdoTemplateTest.Interface;
using EfDbModelDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace EfDbModelDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEvents _eventsRepository;
        public EventsController(ILogger<WeatherForecastController> logger, IEvents eventsRepository)
        {
            _logger = logger;
            _eventsRepository = eventsRepository;
        }

        [HttpGet]
        [Route("getAll")]
        public ActionResult<List<Events>> GetAll()
        {
            try
            {
                return Ok(_eventsRepository.GetAllEvents());
            }
            catch (Exception e)
            {
                return null;// BadRequest(StatusCodes.Status500InternalServerError, "Our Server Issue");
            }
        }

        [HttpPost]
        public IActionResult AddEvent([FromBody]Events eEvent)
        {
            try
            {
                var result =  _eventsRepository.AddEvent(eEvent);

                return result ? Created("", "Event added") : (IActionResult)BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
       
        [HttpGet]
        [Route(("getEvent"))]
        public IActionResult GetEvent(int id)
        {
            try
            {
                return Ok(_eventsRepository.GetEvent(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("deleteEvent")]
        public IActionResult DeleteEvent(int id)
        {
            try
            {
                _eventsRepository.DeleteEvent(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound($"{id} not found");
            }
        }

        [HttpGet()]
        [Route("getOrganizer/{organizer}")]
        public IActionResult GetOrganizer(string organizer)
        {
            try
            {
                var data =_eventsRepository.GetOrganizer(organizer);

                return data != null ? Ok(data) : (IActionResult)NotFound();
            }
            catch (Exception e)
            {
                
            }

            return NotFound();
        }


        [HttpPut]
        [Route("updateEvent")]
        public IActionResult UpdateEvent(int id, [FromBody]Events updatedEvent)
        {
            try
            {
                var result =_eventsRepository.UpdateEvent(id, updatedEvent);
                if ("Updated"== result)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
            }

            return BadRequest();
        }
    }
}
