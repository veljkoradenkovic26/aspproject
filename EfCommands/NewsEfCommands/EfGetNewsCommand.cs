using Application.Commands.News;
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

namespace EfCommands.NewsEfCommands
{
    public class EfGetNewsCommand : BaseCommand,IGetNewsCommand
    {
        public EfGetNewsCommand(NewsContext _context) : base(_context)
        {
        }

        public PageResponse<NewsDto> Execute(NewsSearch request)
        {
            var query = Context.News
                                .Include(nc => nc.NewsCategories)
                                .ThenInclude(c => c.Categories).AsQueryable(); 
            if (request.Keyword != null)
            {
                query = query.Where(n => n.Heading
                .ToLower()
                .Contains(request.Keyword.ToLower()));
            }

            if (request.CategoryName != null)
            {
                query = query
                    .Where(nc => nc.NewsCategories
                            .Any(cat => cat.Categories.Name.ToLower()
                                .Contains(request.CategoryName.Trim().ToLower())));
            }

            var totalRecords = query.Count();

            query = query.Include(u => u.User)
                         .ThenInclude(p => p.Picture)
                         .Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);

            var pagesCount = (int)Math.Ceiling((double)totalRecords / request.PerPage);
            var response = new PageResponse<NewsDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalRecords,
                PagesCount = pagesCount,
                Data = query.Select(p => new NewsDto
                {
                    Id = p.Id,
                    Heading = p.Heading,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,
                    Username = p.User.Username,
                    Path = p.Pictures == null ? "" : p.Pictures.Where(x => x.NewsId == p.Id).FirstOrDefault().Path,
                    NewsDetail = p.NewsCategories.Select(nd => new NewsDetailsDto
                    {
                        CategoryName = nd.Categories.Name,
                        CategoryId = nd.CategoryId
                    }).ToList()
                })
            };

            return response;
         }
    }
}
