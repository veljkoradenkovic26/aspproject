using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.News;
using Application.DataTransfer;
using Application.DataTransfer.Create;
using Application.Exceptions;
using Application.Helpers;
using Application.SearchObjects;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        private IGetNewsCommand _getNewsCommand;
        private IGetNewCommand _getNewCommand;
        private IAddNewsCommand _addNewsCommand;
        private IEditNewsCommand _editNewsCommand;
        private IDeleteNewsCommand _deleteNewsCommand;
        public NewsController(IGetNewsCommand getNewsCommand, IGetNewCommand getNewCommand, IAddNewsCommand addNewsCommand, IDeleteNewsCommand deleteNewsCommand, IEditNewsCommand editNewsCommand)
        {
            _getNewsCommand = getNewsCommand;
            _getNewCommand = getNewCommand;
            _addNewsCommand = addNewsCommand;
            _deleteNewsCommand = deleteNewsCommand;
            _editNewsCommand = editNewsCommand;
        }
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<NewsDto> Get([FromQuery] NewsSearch search)
        {
            try
            {
                var dto = _getNewsCommand.Execute(search);
                return Ok(dto);
            }
            catch(EntityNotFoundException)
            {
                return Conflict("There's no data for your request.");
            }
            
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<NewsDto> Get(int id)
        {
            try
            {
                var dto = _getNewCommand.Execute(id);
                return Ok(dto);
            }
            catch(EntityNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromForm] CreateNewsDto createDto)
        {
            var ext = Path.GetExtension(createDto.Image.FileName); //.gif

            if (!FileUpload.AllowedExtensions.Contains(ext))
            {
                return UnprocessableEntity("Image extension is not allowed.");
            }

            try
            {
                var newFileName = Guid.NewGuid().ToString() + "_" + createDto.Image.FileName;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);

                var pathSave = @"/uploads/" + newFileName;

                createDto.Image.CopyTo(new FileStream(filePath, FileMode.Create));

                createDto.Path = pathSave;
                _addNewsCommand.Execute(createDto);
                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CreateNewsDto newsDto)
        {

            try
            {
                newsDto.NewsId = id;
                _editNewsCommand.Execute(newsDto);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return Conflict("News with that name doesn't exist.");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error has occured.");
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteNewsCommand.Execute(id);
                return NoContent();
            }
            catch(EntityNotFoundException)
            {
                return Conflict("That news is already deleted.");
            }
        }
    }
}
