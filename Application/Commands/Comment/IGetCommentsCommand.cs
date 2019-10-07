using Application.DataTransfer;
using Application.Interfaces;
using Application.Responses;
using Application.SearchObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Comment
{
    public interface IGetCommentsCommand:ICommand<CommentSearch, PageResponse<CommentDto>>
    {
    }
}
