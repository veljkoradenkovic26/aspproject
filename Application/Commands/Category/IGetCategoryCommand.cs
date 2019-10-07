using Application.DataTransfer;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Category
{
    public interface IGetCategoryCommand : ICommand<int, CategoryDto>
    {
    }
}
