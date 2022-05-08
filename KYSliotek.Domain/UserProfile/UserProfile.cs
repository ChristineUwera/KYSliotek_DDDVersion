using KYSliotek.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KYSliotek.Domain.UserProfile
{
    public class UserProfile 
    {
        public UserId Id { get; private set; }

        public FullName FullName { get; private set; }
        public Email Email { get; private set; }

        public UserProfile(UserId id, FullName fullName)
        {

        }
    }
}
