using Application.DataTransfer;
using Application.Interfaces;
using Application.Responses;
using Application.SearchObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Application.Commands.News
{
    public interface IGetNewsCommand : ICommand<NewsSearch, PageResponse<NewsDto>>
    {
    }
}
