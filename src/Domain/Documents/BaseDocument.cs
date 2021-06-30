using System;

namespace DocHelper.Domain.Documents
{
    public abstract class BaseDocument
    {
        public virtual Guid Id { get; protected set; }
        public virtual int ParentId { get; protected set; }
    }
}