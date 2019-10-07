using Application.Commands.Comment;
using Application.DataTransfer;
using Application.Responses;
using Application.SearchObjects;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.CommentEfCommands
{
    public class EfGetCommentsCommand : BaseCommand, IGetCommentsCommand
    {
        public EfGetCommentsCommand(NewsContext _context) : base(_context)
        {
        }

        public PageResponse<CommentDto> Execute(CommentSearch request)
        {
            var comments = Context.Comments.Include(n => n.News).AsQueryable().Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage); 
            if (request.Keyword != null)
            {
                comments = comments.Include(n => n.News).Where(n => n.Email
                .ToLower()
                .Contains(request.Keyword.ToLower()));
            }

            if (request.Name != null)
            {
                comments = comments.Include(n => n.News).Where(n => n.Name == request.Name);
            }

            var totalRecords = comments.Count();
            var pagesCount = (int)Math.Ceiling((double)totalRecords / request.PerPage);

            var response = new PageResponse<CommentDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalRecords,
                PagesCount = pagesCount,
                Data = comments.Select(comment => new CommentDto
                {
                    Email = comment.Email,
                    Id = comment.Id,
                    Comment = comment.Comment,
                    Name = comment.Name,
                    News = new NewsCommentDto
                    {
                        NewsId = comment.News.Id,
                        Heading = comment.News.Heading
                    }
                })
            };

            return response;
        }
    }
}
