using FreightManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace FreightManagement.Domain.ValueObjects
{
    public class Address : ValueObject
    {

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
