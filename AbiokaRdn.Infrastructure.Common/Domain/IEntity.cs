using System;
using System.Collections.Generic;

namespace AbiokaRdn.Infrastructure.Common.Domain
{
    public interface IEntity
    {
        IEnumerable<IEvent> Events { get; }

        DateTime CreatedDate { get; set; }

        DateTime UpdatedDate { get; set; }
    }
}
