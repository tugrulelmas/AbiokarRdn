using System.Collections.Generic;

namespace AbiokaRdn.Infrastructure.Common.Domain
{
    public class Page<T> : IPage<T> where T : class
    {
        public int Count { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
