using Application.DataTransfer;
using Application.DataTransfer.Create;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.News
{
    public interface IEditNewsCommand : ICommand<CreateNewsDto>
    {
    }
}
