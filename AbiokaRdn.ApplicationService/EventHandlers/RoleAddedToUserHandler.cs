using AbiokaRdn.Domain.Events;
using AbiokaRdn.Domain.Repositories;
using AbiokaRdn.Infrastructure.Common.Domain;

namespace AbiokaRdn.ApplicationService.EventHandlers
{
    public class RoleAddedToUserHandler : IEventHandler<RoleAddedToUser>
    {
        private readonly IRoleRepository roleRepository;

        public RoleAddedToUserHandler(IRoleRepository roleRepository) {
            this.roleRepository = roleRepository;
        }

        public int Order => 5;

        public void Handle(RoleAddedToUser eventInstance) {
            roleRepository.AddToUser(eventInstance.RoleId, eventInstance.User.Id);
        }
    }
}
