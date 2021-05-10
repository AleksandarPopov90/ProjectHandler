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
    public class ProjectController : ControllerBase
    {
        private ProjectHandlerContext _context;

        public ProjectController(ProjectHandlerContext context)
        {
            _context = context;
        }

        // GET: api/<ProjectController>
        /// <summary>
        /// Get all projects
        /// </summary>
        /// <response code="200">Returns all projects from database</response>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Project> Get()
        {
            return Ok(_context.Projects);
        }
        // GET: api/<ProjectController>
        /// <summary>
        /// Get projects by given filter
        /// </summary>
        /// <param name="date"></param>
        /// <param name="priority"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpGet("{date}/{priority}/{sort}/{order}")]
        public ActionResult<Project> GetByFilter(DateTime date, int priority, int sort, bool order)
        {
            List<Project> items = new();
            if (sort == 1)
            {
                if (order == true)
                    items = _context.Projects.OrderBy(x => x.ProjectName).Where(x => x.ProjectStartDate == date && x.ProjectPriority == priority).ToList();
                else
                    items = _context.Projects.OrderByDescending(x => x.ProjectName).Where(x => x.ProjectStartDate == date && x.ProjectPriority == priority).ToList();
            }
            else if (sort == 2)
            {
                if (order == true)
                    items = _context.Projects.OrderBy(x => x.ProjectStartDate).Where(x => x.ProjectStartDate == date && x.ProjectPriority == priority).ToList();
                else
                    items = _context.Projects.OrderByDescending(x => x.ProjectStartDate).Where(x => x.ProjectStartDate == date && x.ProjectPriority == priority).ToList();
            }
            else if (sort == 3)
            {
                if (order == true)
                    items = _context.Projects.OrderBy(x => x.ProjectPriority).Where(x => x.ProjectStartDate == date && x.ProjectPriority == priority).ToList();
                else
                    items = _context.Projects.OrderByDescending(x => x.ProjectPriority).Where(x => x.ProjectStartDate == date && x.ProjectPriority == priority).ToList();
            }
            return Ok(items);
        }
        // GET: api/<ProjectController>
        /// <summary>
        /// Get all tasks for given project Id
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <response code="200">Returns all tasks from database for single project</response>
        /// <returns></returns>
        [HttpGet("{ProjectId}")]
        public ActionResult<Project> GetAllTasksInProject(int ProjectId)
        {
            return Ok(_context.Tasks.Where(x => x.ProjectId == ProjectId));
        }
        // GET api/<ProjectController>/5
        /// <summary>
        /// Get single project for given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Project> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id can't be 0 or less.");
            }

            var result = _context.Projects.SingleOrDefault(x => x.Id == id);

            if (result == null)
            {
                return NotFound("Nothing found with id : " + id);
            }
            return Ok(result);
        }

        // POST api/<ProjectController>
        /// <summary>
        /// Create new project
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<string> Post([FromBody] Project value)
        {
            if (value == null || value.ProjectStatus == null)
            {
                return BadRequest("Model can't be null");
            }
            
            _context.Projects.Add(value);
            var result = _context.SaveChanges();

            return Ok($"Succsesfully added {result} rows.");

        }

        // PUT api/<ProjectController>/5
        /// <summary>
        /// Update\change existing project informations
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<string> Put([FromBody] Project value)
        {
            if (value == null || value.ProjectStatus == null)
            {
                return BadRequest("Model can't be null");
            }
            _context.Projects.Update(value);
            var result = _context.SaveChanges();

            return Ok($"Successfully updated {result} rows.");
        }

        // DELETE api/<ProjectController>/5
        /// <summary>
        /// Delete project by given Id
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
            var item = _context.Projects.FirstOrDefault(x => x.Id == id);

            if (item != null) 
            {
                var tasks = _context.Tasks.Where(x => x.ProjectId == item.Id);
                _context.Tasks.RemoveRange(tasks);
                _context.Projects.Remove(item);
                var result = _context.SaveChanges();
                return Ok($"Succsesfully deleted {result} rows.");
            }
            if (item == null) 
            {
                return NotFound($"Item with {id} not found.");
            }
            return Ok("Successfuly deleted.");
        }
    }
}
