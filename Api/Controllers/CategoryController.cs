using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Category;
using Application.DataTransfer;
using Application.DataTransfer.Create;
using Application.Exceptions;
using Application.SearchObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private IGetCategoryCommand _getCategoryCommand;
        private IGetCategoriesCommand _getCategoriesCommand;
        private IAddCategoryCommand _addCategoryCommand;
        private IEditCategoryCommand _editCategoryCommand;
        private IDeleteCategoryCommand _deleteCategoryCommand;
        public CategoryController(IGetCategoryCommand getCategoryCommand, IGetCategoriesCommand getCategoriesCommand, IAddCategoryCommand addCategoryCommand, IEditCategoryCommand editCategoryCommand, IDeleteCategoryCommand deleteCategoryCommand)
        {
            _getCategoriesCommand = getCategoriesCommand;
            _getCategoryCommand =  getCategoryCommand;
            _addCategoryCommand = addCategoryCommand;
            _editCategoryCommand = editCategoryCommand;
            _deleteCategoryCommand = deleteCategoryCommand;
        }
        // GET: api/Category
        [HttpGet]
        public IActionResult Get([FromQuery] CategorySearch search)
        {
            try
            {
                var dto = _getCategoriesCommand.Execute(search);
                return Ok(dto);
            }
            catch (EntityNotFoundException)
            {
                return Conflict("There's no data for your request.");
            }
        }

        // GET: api/Category/5
        [HttpGet("{id}", Name = "GetCategory")]
        public ActionResult<CategoryDto> Get(int id)
        {
            try
            {
                var dto = _getCategoryCommand.Execute(id);
                return Ok(dto);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: api/Category
        [HttpPost]
        public IActionResult Post([FromBody] CreateCategoryDto categoryDto)
        {
            try
            {
                _addCategoryCommand.Execute(categoryDto);
                return StatusCode(201);

            }
            catch
            {
                return StatusCode(500, "An error has occured.");
            }
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreateCategoryDto category)
        {
            category.CategoryId = id;
            try
            {

                _editCategoryCommand.Execute(category);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                if (e.Message == "Category doesn't exist.")
                {
                    return NotFound(e.Message);
                }

                return UnprocessableEntity(e.Message);

            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteCategoryCommand.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                if (e.Message == "Category doesn't exist.")
                {
                    return NotFound(e.Message);
                }

                return UnprocessableEntity(e.Message);
            }
        }
    }
}
