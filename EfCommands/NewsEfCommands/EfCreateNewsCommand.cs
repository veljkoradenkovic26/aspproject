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
    public class EfCreateNewsCommand : BaseCommand, IAddNewsCommand
    {
        public EfCreateNewsCommand(NewsContext _context) : base(_context)
        {
        }

        public void Execute(CreateNewsDto request)
        {
            if (request.CategoryIds.Any(x => !Context.Categories.Any(y => y.Id == x)))
            {
                throw new EntityNotFoundException("Categories");
            }
            var createNewNew = CreateNewNews(request);
            CreateNewNewsCategory(request,createNewNew);
            SavePicture(request, createNewNew);
        }

        private void SavePicture(CreateNewsDto request, int newsId)
        {
            var pictureId = 0;
            var picture = new Picture
            {
                Path = request.Path,
                CreatedAt = DateTime.Now,
                NewsId = newsId
            };
            Context.Pictures.Add(picture);
            Context.SaveChanges();
        }

        private int CreateNewNews(CreateNewsDto request)
        {
            int newsId;
            var news = new News
            {
                Heading = request.Heading,
                Content = request.Content,
                CreatedAt = DateTime.Now,
                UserId = /*request.UserId*/1
            };
            Context.News.Add(news);
            Context.SaveChanges();
            newsId = news.Id;
            return newsId;
        }

        private void CreateNewNewsCategory(CreateNewsDto request, int id)
        {
            request.NewsId = id;
            RemoveExistsRelastionship(request);

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

        private void RemoveExistsRelastionship(CreateNewsDto request)
        {
            var foundNewsCategories = Context.NewsCategories.Where(x => x.NewsId == request.NewsId);
            Context.NewsCategories.RemoveRange(foundNewsCategories);
            Context.SaveChanges();
        }
    }
}
