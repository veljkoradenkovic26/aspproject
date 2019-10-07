using Application.Commands.News;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.NewsEfCommands
{
    public class EfDeleteNewsCommand : BaseCommand, IDeleteNewsCommand
    {
        public EfDeleteNewsCommand(NewsContext _context) : base(_context)
        {
        }

        public void Execute(int request)
        {
            if (request == 0)
                throw new NullReferenceException();
            var foundedNew = Context.News.Find(request);
            try
            {
                Context.News.Remove(foundedNew);
                Context.SaveChanges();
            }
            catch
            {
                throw new EntityNotFoundException("News");
            }
        }
    }
}
