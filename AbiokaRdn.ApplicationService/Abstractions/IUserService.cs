﻿using AbiokaRdn.ApplicationService.DTOs;
using AbiokaRdn.ApplicationService.Messaging;
using AbiokaRdn.Infrastructure.Common.Authentication;
using System;

namespace AbiokaRdn.ApplicationService.Abstractions
{
    public interface IUserService : IReadService<UserDTO>
    {
        /// <summary>
        /// Adds the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [AllowedRole("Admin")]
        AddUserResponse Add(AddUserRequest request);

        /// <summary>
        /// Registers the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        AddUserResponse Register(RegisterUserRequest request);

        /// <summary>
        /// Updates the specified entiy.
        /// </summary>
        /// <param name="entiy">The entiy.</param>
        [AllowedRole("Admin")]
        void Update(UserDTO entiy);

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [AllowedRole("Admin")]
        void Delete(Guid id);

        /// <summary>
        /// Count of users.
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>New token</returns>
        string ChangePassword(ChangePasswordRequest request);

        /// <summary>
        /// Changes the language.
        /// </summary>
        /// <param name="language">The language.</param>
        void ChangeLanguage(string language);

        /// <summary>
        /// Verifies the email.
        /// </summary>
        /// <param name="encodedToken">The encoded token.</param>
        void VerifyEmail(string encodedToken);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        void ResetPassword(string email);

        /// <summary>
        /// News the password.
        /// </summary>
        /// <param name="request">The request.</param>
        void NewPassword(NewPasswordRequest request);
    }
}
