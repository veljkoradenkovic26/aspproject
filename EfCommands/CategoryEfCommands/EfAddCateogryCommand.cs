using Application.Commands.Category;
using Application.DataTransfer.Create;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.CategoryEfCommands
{
    public class EfAddCateogryCommand : BaseCommand, IAddCategoryCommand
    {
        public EfAddCateogryCommand(NewsContext _context) : base(_context)
        {
        }

        public void Execute(CreateCategoryDto request)
        {
            if (request == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                try
                {
                    Context.Categories.Add(new Domain.Category {
                        Name = request.CategoryName,
                        CreatedAt = DateTime.Now
                    });
                    Context.SaveChanges();
                }
                catch
                {
                    throw new NullReferenceException();
                }
            }
        }
    }
}
