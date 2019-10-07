using Application.Commands.Category;
using Application.DataTransfer;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.CategoryEfCommands
{
    public class EfGetCategoryCommand : BaseCommand, IGetCategoryCommand
    {
        public EfGetCategoryCommand(NewsContext _context) : base(_context)
        {
        }

        public CategoryDto Execute(int request)
        {
            var category = Context.Categories.Find(request);
            if (category == null)
                throw new EntityNotFoundException();

            return new CategoryDto
            {
                Id = category.Id,
                CreatedAt = category.CreatedAt,
                Name = category.Name
            };
        }
    }
}
