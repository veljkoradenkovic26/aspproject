using Application.Commands.Category;
using Application.DataTransfer;
using Application.DataTransfer.Create;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.CategoryEfCommands
{
    public class EfEditCategoryCommand : BaseCommand, IEditCategoryCommand
    {
        public EfEditCategoryCommand(NewsContext _context) : base(_context)
        {
        }

        public void Execute(CreateCategoryDto request)
        {
            var category = Context.Categories.Find(request.CategoryId);
            if(category == null)
            {
                throw new EntityNotFoundException("Category");
            }
            if(Context.Categories.Any(x => x.Name == request.CategoryName)){
                throw new EntityAlreadyExistsException("Category");
            }
            category.Name = request.CategoryName;
            category.ModifiedAt = DateTime.Now;
            Context.SaveChanges();

        }
    }
}
