using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvertisingManagement.BLL
{
    public class DataTransformException : ApplicationException
    {
        public DataTransformException()
        {
        }

        public DataTransformException(string message)
            : base(message)
        {
        }

        public DataTransformException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
