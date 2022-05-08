using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KYSliotek.Commands
{
    //commands comming from outside
    public static class Contracts
    {
        public static class V1
        {
            public class Create
            {
                public Guid Id { get; set; }
                public Guid AuthorId { get; set; }
            }

            public class SetTitle
            {
                public Guid Id { get; set; }
                public string Title { get; set; }
            }

            public class UpdateDescription
            {
                public Guid Id { get; set; }
                public string Description { get; set; }
            }

            public class RequestToPublish
            {
                public Guid Id { get; set; }
            }

            public class Publish
            {
                public Guid Id { get; set; }
                public Guid ApprovedBy { get; set; }
            }
        }

    }
}
