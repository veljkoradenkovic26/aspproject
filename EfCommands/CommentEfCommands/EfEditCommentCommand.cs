using Application.Commands.Comment;
using Application.DataTransfer.Create;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.CommentEfCommands
{
    public class EfEditCommentCommand : BaseCommand, IEditCommentCommand
    {
        public EfEditCommentCommand(NewsContext _context) : base(_context)
        {
        }

        public void Execute(CreateCommentDto request)
        {
            var comment = Context.Comments.Find(request.CommentId);
            if (comment == null)
            {
                throw new EntityNotFoundException("Comment");
            }
            if (!Context.News.Any(x => x.Id == request.NewsId))
            {
                throw new EntityNotFoundException("News");
            }
            comment.Name = request.Username;
            comment.ModifiedAt = DateTime.Now;
            comment.Email = request.Email;
            comment.Comment = request.Text;
            comment.NewsId = request.NewsId;
            Context.SaveChanges();
        }
    }
}
