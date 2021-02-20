using System;


namespace FreightManagement.Domain.Exceptions
{
    public class DuplicateNameException : Exception 
    {
        public DuplicateNameException(string message) : base(message)
        {

        }
    }
}
