using Application.Commands.Category;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Responses;
using Application.SearchObjects;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.CategoryEfCommands
{
    public class EfGetCategoriesCommand : BaseCommand, IGetCategoriesCommand
    {
        public EfGetCategoriesCommand(NewsContext _context) : base(_context)
        {
        }

        public PageResponse<CategoryDto> Execute(CategorySearch request)
        {
            
            if (request != null)
            {
                var query = Context.Categories.AsQueryable().Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);
                var totalRecords = query.Count();
                var pagesCount = (int)Math.Ceiling((double)totalRecords / request.PerPage);
                if (request.Keyword != null)
                {
                    query = query.Where(n => n.Name
                    .ToLower()
                    .Contains(request.Keyword.ToLower()));
                }
                totalRecords = query.Count();
                var response = new PageResponse<CategoryDto>
                {
                    CurrentPage = request.PageNumber,
                    TotalCount = totalRecords,
                    PagesCount = pagesCount,
                    Data = query.Select(c => new CategoryDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        CreatedAt = c.CreatedAt
                    })
                };

                return response;
            }
            else
            {
                var query = Context.Categories.AsQueryable();
                var totalRecords = query.Count();
                var pagesCount = (int)Math.Ceiling((double)totalRecords / 4);
                return new PageResponse<CategoryDto>
                {
                    CurrentPage = 1,
                    TotalCount = totalRecords,
                    PagesCount = pagesCount,
                    Data = query.Select(c => new CategoryDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        CreatedAt = c.CreatedAt
                    })
                };
            }
        }
    }
}
