using Application.Commands.User;
using Application.DataTransfer;
using Application.Exceptions;
using Application.SearchObjects;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.UserEfCommands
{
    public class EfGetUserCommnd : BaseCommand, IGetUserFromLoginForm
    {
        public EfGetUserCommnd(NewsContext _context) : base(_context)
        {
        }

        public UserDto Execute(UserSearch request)
        {
            if(request != null)
            {
                var user = Context.Users.Include(x => x.Role).AsQueryable();
                var password = HashPasswordCommand.MD5Hash(request.Password);
                var founded = Context.Users.Include(x=> x.Role).Where(x => x.Username == request.Username && x.Password == password).FirstOrDefault();
                if (founded == null)
                {
                    throw new EntityNotFoundException("User");
                }

                return new UserDto
                {
                    FirstName = founded.FirstName,
                    LastName = founded.LastName,
                    Email = founded.Email,
                    Username = founded.Username,
                    Id = founded.Id,
                    RoleName = founded.Role.Name
                };
            }
            else
            {
                throw new EntityNotFoundException("User");
            }
        }
    }
}
