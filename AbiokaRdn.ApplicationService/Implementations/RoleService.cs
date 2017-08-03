using AbiokaRdn.ApplicationService.Abstractions;
using AbiokaRdn.ApplicationService.DTOs;
using AbiokaRdn.Domain;
using AbiokaRdn.Domain.Repositories;
using AbiokaRdn.Infrastructure.Common.Authentication;
using AbiokaRdn.Infrastructure.Common.Domain;

namespace AbiokaRdn.ApplicationService.Implementations
{
    public class RoleService : CrudService<Role, RoleDTO>, IRoleService
    {
        public RoleService(IRoleRepository repository, IDTOMapper dtoMapper) 
            : base(repository, dtoMapper) {
        }

        [AllowedRole("Admin")]
        public override void Add(RoleDTO entity) {
            base.Add(entity);
        }

        [AllowedRole("Admin")]
        public override void Delete(object id) {
            base.Delete(id);
        }

        [AllowedRole("Admin")]
        public override void Update(RoleDTO entity) {
            base.Update(entity);
        }

        [AllowedRole("Admin")]
        public override IPage<RoleDTO> GetWithPage(int page, int limit, string order) {
            return base.GetWithPage(page, limit, order);
        }
    }
}
