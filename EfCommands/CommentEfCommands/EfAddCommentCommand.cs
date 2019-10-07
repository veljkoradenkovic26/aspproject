using Application.Commands.Comment;
using Application.DataTransfer.Create;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.CommentEfCommands
{
    public class EfAddCommentCommand : BaseCommand, IAddCommentCommand
    {
        public EfAddCommentCommand(NewsContext _context) : base(_context)
        {
        }

        public void Execute(CreateCommentDto request)
        {
            if (request == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                try
                {
                    var comment = new Comments
                    {
                        Name = request.Username,
                        Comment = request.Text,
                        NewsId = request.NewsId,
                        Email = request.Email,
                        CreatedAt = DateTime.Now
                    };
                    Context.Comments.Add(comment);
                    Context.SaveChanges();
                }
                catch(Exception e)
                {
                    throw new NullReferenceException();
                }
            }
        }
    }
}
