using KYSliotek.Framework;
using System;

namespace KYSliotek.Domain.LentingServices
{
    public class LendingId : Value<LendingId>
    {
        public Guid Value { get; private set; }

        public LendingId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Lending id cannot be empty");

            Value = value;
        }

        public static implicit operator Guid(LendingId self) => self.Value;

        public static implicit operator LendingId(string value)
           => new LendingId(Guid.Parse(value));

        public override string ToString() => Value.ToString();

        //for serialization
        protected LendingId() { }
    }
}
