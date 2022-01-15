using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Application.Exceptions
{
    public class DuplicateException : ApplicationException
    {
        public DuplicateException( string name )
            : base($"Your \"{name}\" is invalid(duplicate value).")
        {
        }
        
    }
}
