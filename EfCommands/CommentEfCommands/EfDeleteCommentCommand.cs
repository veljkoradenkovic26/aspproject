using Application.Commands.Comment;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.CommentEfCommands
{
    public class EfDeleteCommentCommand : BaseCommand, IDeleteCommentCommand
    {
        public EfDeleteCommentCommand(NewsContext _context) : base(_context)
        {
        }

        public void Execute(int request)
        {
            if (request == 0)
                throw new NullReferenceException();
            var foundedComment = Context.Comments.Find(request);
            try
            {
                Context.Comments.Remove(foundedComment);
                Context.SaveChanges();
            }
            catch
            {
                throw new EntityNotFoundException("Comment");
            }
        }
    }
}
