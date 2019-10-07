using Application.DataTransfer.Create;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Comment
{
    public interface IEditCommentCommand : ICommand<CreateCommentDto>
    {
    }
}
