using Application.DataTransfer.Create;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Category
{
    public interface IAddCategoryCommand : ICommand<CreateCategoryDto>
    {
    }
}
