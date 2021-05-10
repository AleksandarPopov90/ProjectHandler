using Microsoft.AspNetCore.Mvc;
using ProjectHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectHandler.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ProjectHandlerContext _context;
        public TaskController(ProjectHandlerContext context)
        {
            _context = context;
        }
        // GET: api/<TaskController>
        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Models.Task> Get()
        {
            return Ok(_context.Tasks);
        }

        // GET api/<TaskController>/5
        /// <summary>
        /// Get single task by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Models.Task> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id can't be 0 or less.");
            }

            var result = _context.Tasks.SingleOrDefault(x => x.Id == id);

            if (result == null)
            {
                return NotFound("Nothing found with id : " + id);
            }
            return Ok(result);
        }

        // POST api/<TaskController>
        /// <summary>
        /// Create new task for existing project
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<string> Post([FromBody] Models.Task value)
        {
            if (value == null || value.TaskStatus == null)
            {
                return BadRequest("Model can't be null");
            }

            _context.Tasks.Add(value);
            var result = _context.SaveChanges();

            return Ok($"Succsesfully added {result} rows.");
        }

        // PUT api/<TaskController>/5
        /// <summary>
        /// Update\change existing task informations
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<string> Put([FromBody] Models.Task value)
        {
            if (value == null || value.TaskStatus == null)
            {
                return BadRequest("Model can't be null");
            }
            _context.Tasks.Update(value);
            var result = _context.SaveChanges();

            return Ok($"Succsesfully updated {result} rows.");
        }

        // DELETE api/<TaskController>/5
        /// <summary>
        /// Delete task by given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id can't be 0 or less.");
            }

            var item = _context.Tasks.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return NotFound($"Item with {id} not found.");
            }
            if (item != null)
            {
                _context.Tasks.Remove(item);
                var result = _context.SaveChanges();
            }
            return Ok("Successfuly deleted.");
            
        }
    }
}
