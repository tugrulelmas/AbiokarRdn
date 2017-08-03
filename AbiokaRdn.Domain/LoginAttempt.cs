using AbiokaRdn.Infrastructure.Common.Domain;
using System;

namespace AbiokaRdn.Domain
{
    public class LoginAttempt : IdEntity<Guid>
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public virtual string Token { get; set; }

        /// <summary>
        /// Gets or sets the ip.
        /// </summary>
        /// <value>
        /// The ip.
        /// </value>
        public virtual string IP { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the login result.
        /// </summary>
        /// <value>
        /// The login result.
        /// </value>
        public virtual LoginResult LoginResult { get; set; }
    }
}
