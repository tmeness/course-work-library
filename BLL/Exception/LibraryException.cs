using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LibraryException : Exception
    {
        private string message;
        public LibraryException() : base(){ }

        public LibraryException(string message)
        {
            this.message = message;
        }

        public override string Message => message;
    }
}
