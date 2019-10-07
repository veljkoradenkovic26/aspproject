using Application.Commands.User;
using Application.DataTransfer;
using Application.Responses;
using Application.SearchObjects;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands.UserEfCommands
{
    public class EfGetUsersCommand : BaseCommand, IGetUsersCommand
    {
        public EfGetUsersCommand(NewsContext _context) : base(_context)
        {
        }

        public PageResponse<UserDto> Execute(UserSearch request)
        {
            var users = Context.Users.Include(r => r.Role).AsQueryable().Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);
            if (request.Username != null)
            {
                users = users.Where(n => n.Username
                .ToLower()
                .Contains(request.Username.ToLower()));
            }

            var totalRecords = users.Count();
            var pagesCount = (int)Math.Ceiling((double)totalRecords / request.PerPage);

            var response = new PageResponse<UserDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalRecords,
                PagesCount = pagesCount,
                Data = users.Select(user => new UserDto
                {
                    FirstName = user.FirstName,
                    Username = user.Username,
                    Email = user.Email,
                    LastName = user.LastName,
                    RoleName = user.Role.Name,
                    PictureId = user.PictureId
                })
            };

            return response;
        }
    }
}
