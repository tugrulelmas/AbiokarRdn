using AbiokaRdn.Domain.Events;
using AbiokaRdn.Domain.Repositories;
using AbiokaRdn.Infrastructure.Common.Domain;

namespace AbiokaRdn.ApplicationService.EventHandlers
{
    public class RoleRemovedFromUserHandler : IEventHandler<RoleRemovedFromUser>
    {
        private readonly IRoleRepository roleRepository;

        public RoleRemovedFromUserHandler(IRoleRepository roleRepository) {
            this.roleRepository = roleRepository;
        }

        public int Order => 5;

        public void Handle(RoleRemovedFromUser eventInstance) {
            roleRepository.RemoveFromUser(eventInstance.RoleId, eventInstance.User.Id);
        }
    }
}
