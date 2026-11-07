using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApi.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public int? CreatedById { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        public int? UpdatedById { get; protected set; }
        public DateTime? DateUpdated { get; protected set; }
    }
}
