using AbiokaRdn.ApplicationService.DTOs;
using System.Collections.Generic;

namespace AbiokaRdn.ApplicationService.Abstractions
{
    public interface IMenuService : ICrudService<MenuDTO>
    {
        IEnumerable<MenuDTO> Filter(string text);
    }
}