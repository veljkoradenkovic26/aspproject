﻿using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.News
{
    public interface IDeleteNewsCommand : ICommand<int>
    {
    }
}
