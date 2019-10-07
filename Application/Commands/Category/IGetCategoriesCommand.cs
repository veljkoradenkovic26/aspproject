using Application.DataTransfer;
using Application.Interfaces;
using Application.Responses;
using Application.SearchObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Category
{
    public interface IGetCategoriesCommand : ICommand<CategorySearch,PageResponse<CategoryDto>>
    {
    }
}
