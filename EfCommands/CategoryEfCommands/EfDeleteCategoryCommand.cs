using Application.Commands.Category;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.CategoryEfCommands
{
    public class EfDeleteCategoryCommand : BaseCommand, IDeleteCategoryCommand
    {
        public EfDeleteCategoryCommand(NewsContext _context) : base(_context)
        {
        }

        public void Execute(int request)
        {
            if (request == 0)
                throw new NullReferenceException();
            var foundedCategory = Context.Categories.Find(request);
            try
            {
                Context.Categories.Remove(foundedCategory);
                Context.SaveChanges();
            }
            catch
            {
                throw new EntityNotFoundException("Category");
            }
        }
    }
}
