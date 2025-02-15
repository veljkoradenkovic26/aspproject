﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Responses
{
    public class PageResponse<T> : List<T>
    {
        public int TotalCount { get; set; }
        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
