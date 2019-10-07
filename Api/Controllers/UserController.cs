using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.User;
using Application.Exceptions;
using Application.SearchObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IGetUsersCommand _getUsersCommand;
        private IDeleteUserCommand _deleteUsersCommand;

        public UserController(IGetUsersCommand getUsersCommand)
        {
            _getUsersCommand = getUsersCommand;
        }

        // GET: api/User
        [HttpGet]
        public IActionResult Get([FromQuery] UserSearch search)
        {
            try
            {
                var dto = _getUsersCommand.Execute(search);
                return Ok(dto);
            }
            catch (EntityNotFoundException)
            {
                return Conflict("There's no data for your request.");
            }
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "GetUser")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteUsersCommand.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return Conflict("That news is already deleted.");
            }
        }
    }
}
