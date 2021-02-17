using FreightManagement.Domain.Common;
using System;
using System.Collections.Generic;


// https://conductofcode.io/post/entities-and-value-objects-in-csharp-for-ddd/

namespace FreightManagement.Domain.ValueObjects
{
    public class Email : ValueObject
    {

        public string Value { get; }

        public Email(string value)
        {
            if (!value.Contains("@")) throw new Exception("Email is invalid");

            Value = value;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Email;

            return other != null ? Equals(other) : Equals(obj as string);
        }

        public bool Equals(Email other) => other != null && Value == other.Value;

        public bool Equals(string other) => Value == other;

           public override string ToString() => Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Value;
        }

        public override int GetHashCode()
        {
           return  base.GetHashCode();
        }
    }

}
