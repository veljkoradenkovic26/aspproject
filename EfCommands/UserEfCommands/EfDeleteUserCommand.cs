using Application.Commands.User;
using Application.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands.UserEfCommands
{
    public class EfDeleteUserCommand : BaseCommand, IDeleteUserCommand
    {
        public EfDeleteUserCommand(NewsContext _context) : base(_context)
        {
        }

        public void Execute(int request)
        {
            if (request == 0)
                throw new NullReferenceException();
            var foundedUser = Context.Users.Find(request);
            try
            {
                Context.Users.Remove(foundedUser);
                Context.SaveChanges();
            }
            catch
            {
                throw new EntityNotFoundException("User");
            }
        }
    }
}
