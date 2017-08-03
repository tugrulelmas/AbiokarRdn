﻿using AbiokaRdn.Infrastructure.Common.Domain;
using System;
using System.Collections.Generic;

namespace AbiokaRdn.Domain
{
    public class Menu : IdEntity<Guid>
    {
        public Menu() {

        }

        public Menu(Guid id, string text, string url, short order, Menu parent, Role role, IEnumerable<Menu> children) {
            Id = id;
            Text = text;
            Url = url;
            Order = order;
            Parent = parent;
            Role = role;
            Children = children;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public virtual string Text { get; protected set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public virtual string Url { get; protected set; }

        public virtual short Order { get; protected set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public virtual Menu Parent { get; protected set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public virtual Role Role { get; protected set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public virtual IEnumerable<Menu> Children { get; protected set; }

        public static Menu Create(Guid id) => new Menu(id, null, null, 0, null, null, null);
    }
}
