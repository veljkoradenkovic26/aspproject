using Application.Commands.News;
using Application.DataTransfer.Create;
using Application.Exceptions;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.NewsEfCommands
{
    public class EfEditNewsCommand : BaseCommand, IEditNewsCommand
    {
        public EfEditNewsCommand(NewsContext _context) : base(_context)
        {
        }

        public void Execute(CreateNewsDto request)
        {
            var news = Context.News.Find(request.NewsId);
            if (news == null)
            {
                throw new EntityNotFoundException("News");
            }
            if(request.CategoryIds.Any(x => !Context.Categories.Any(y => y.Id == x)))
            {
                throw new EntityNotFoundException("Categories");
            }
            //if (!Context.Users.Any(x => x.Id == HttpContext.UserId))
            //{
            //    throw new EntityNotFoundException("User");
            //}

            var picture = new Picture
            {
                Path = request.Path,
                CreatedAt = DateTime.Now,
                NewsId = request.NewsId
            };

            news.Heading = request.Heading;
            news.Content = request.Content;
            news.ModifiedAt = DateTime.Now;
            news.UserId = /*request.UserId*/1;
            Context.SaveChanges();

            Context.Pictures.Add(picture);

            var foundNewsCategories = Context.NewsCategories.Where(x => x.NewsId == request.NewsId);
            Context.NewsCategories.RemoveRange(foundNewsCategories);
            Context.SaveChanges();

            foreach (var cat in request.CategoryIds)
            {
                var newsCategories = new NewsCategories()
                {
                    CategoryId = cat,
                    NewsId = request.NewsId
                };
                Context.NewsCategories.Add(newsCategories);
                Context.SaveChanges();
            }
        }
    }
}
