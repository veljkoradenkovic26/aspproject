using Application.Commands.Comment;
using Application.DataTransfer;
using Application.Exceptions;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.CommentEfCommands
{
    public class EfGetCommentCommand : BaseCommand, IGetCommentCommand
    {
        public EfGetCommentCommand(NewsContext _context) : base(_context)
        {
        }

        public CommentDto Execute(int request)
        {

            var comments = Context.Comments.Include(n => n.News).AsQueryable();

            var commentsDb = comments.Where(x => x.Id == request).FirstOrDefault();
            if (commentsDb == null)
                throw new EntityNotFoundException("Comment");

            return new CommentDto
            {
                Email = commentsDb.Email,
                Id = commentsDb.Id,
                Comment = commentsDb.Comment,
                Name = commentsDb.Name,
                News = new NewsCommentDto
                {
                    NewsId = commentsDb.News.Id,
                    Heading = commentsDb.News.Heading
                }
            };

        }
    }
}
