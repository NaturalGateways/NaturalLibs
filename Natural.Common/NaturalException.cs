using System;
using System.Collections.Generic;
using System.Text;

namespace Natural
{
    /// <summary>Exception class for known exceptions in the Natural libraries.</summary>
    public class NaturalException : Exception
    {
        /// <summary>Constructor.</summary>
        public NaturalException(string message)
            : base(message)
        {
            //
        }

        /// <summary>Constructor.</summary>
        public NaturalException(string message, Exception ex)
            : base(message, ex)
        {
            //
        }
    }
}
