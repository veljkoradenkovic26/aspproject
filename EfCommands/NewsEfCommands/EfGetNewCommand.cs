using Application.Commands.News;
using Application.DataTransfer;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.NewsEfCommands
{
    public class EfGetNewCommand : BaseCommand, IGetNewCommand
    {
        public EfGetNewCommand(NewsContext _context) : base(_context)
        {
        }

        public NewsDto Execute(int request)
        {

            var news = Context.News.AsQueryable();

            var newsDb = news.Include(u => u.User)
                         .ThenInclude(p => p.Picture)
                                .Include(nc => nc.NewsCategories)
                                .ThenInclude(c => c.Categories).Where(x => x.Id == request).FirstOrDefault();
            if (newsDb == null)
                throw new EntityNotFoundException("News");

            var dto = new NewsDto
            {
                Id = newsDb.Id,
                Heading = newsDb.Heading,
                Content = newsDb.Content,
                Username = newsDb.User.Username,
                Path = !newsDb.Pictures.Any() ? "" : newsDb.Pictures.Where(x=> x.NewsId == request).FirstOrDefault().Path ,
                CreatedAt = newsDb.CreatedAt,
                NewsDetail = newsDb.NewsCategories.Select(nd => new NewsDetailsDto {
                    CategoryName = nd.Categories.Name,
                    CategoryId = nd.CategoryId
                }).ToList()
            };

            return dto;
        }
    }
}
