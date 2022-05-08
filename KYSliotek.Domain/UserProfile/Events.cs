using System;
using System.Collections.Generic;
using System.Text;

namespace KYSliotek.Domain.UserProfile
{
    public static class Events
    {
        public class UserRegistered
        {
            public Guid UserId { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
        }

        public class UserFullNameUpdated
        {
            public Guid UserId { get; set; }
            public string FullName { get; set; }
        }

        public class UserEmailUpdated
        {
            public Guid UserId { get; set; }
            public string Email { get; set; }
        }
    }
}
