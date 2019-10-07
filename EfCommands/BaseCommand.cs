using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class BaseCommand
    {
        protected NewsContext Context { get; }
        public BaseCommand(NewsContext _context)
        {
            Context = _context;
        }
    }
}
