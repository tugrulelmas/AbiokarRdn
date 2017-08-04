using AbiokaRdn.ApplicationService.Abstractions;
using AbiokaRdn.ApplicationService.DTOs;
using AbiokaRdn.ApplicationService.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AbiokaRdn.Host.Controllers
{
    [Route("api/User")]
    public class UserController : BaseReadController<UserDTO>
    {
        private readonly IUserService userService;
        private readonly INotificationService notificationService;

        public UserController(IUserService userService, INotificationService notificationService)
            : base(userService) {
            this.userService = userService;
            this.notificationService = notificationService;
        }
        
        [HttpPost]
        public IActionResult Add([FromBody]AddUserRequest request) {
            var user = userService.Add(request);

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterUserRequest request) {
            var user = userService.Register(request);

            return Ok(user);
        }

        [HttpPut]
        public IActionResult Update([FromBody]UserDTO user) {
            userService.Update(user);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id) {
            userService.Delete(id);

            return Ok();
        }

        [HttpPut("{id}/ChangePassword")]
        public IActionResult ChangePassword(Guid id, [FromBody]ChangePasswordRequest request) {
            request.UserId = id;
            var newToken = userService.ChangePassword(request);

            return Ok(newToken);
        }

        [HttpPut("{id}/ChangeLanguage")]
        public IActionResult ChangeLanguage(Guid id, string language) {
            userService.ChangeLanguage(language);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("{id}/Verify")]
        public IActionResult Verify(string id) {
            userService.VerifyEmail(id);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("{id}/ResendVerification")]
        public IActionResult ResendVerification(string id) {
            notificationService.SendVerificationEmail(new SendVerificationEmailRequest { Email = id });

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("{id}/ResetPassword")]
        public IActionResult ResetPassword(string id) {
            userService.ResetPassword(id);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("{id}/NewPassword")]
        public IActionResult NewPassword(string id, [FromBody]NewPasswordRequest request) {
            request.Token = id;
            userService.NewPassword(request);

            return Ok();
        }
    }
}
