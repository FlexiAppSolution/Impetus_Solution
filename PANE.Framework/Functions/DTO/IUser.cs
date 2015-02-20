using System;
using System.Collections.Generic;
using System.Text;
using PANE.Framework.DTO;
using PANE.Framework.Functions.DTO;

namespace PANE.Framework.Functions.DTO
{
    public interface IUser
    {
        System.Int64 ID { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        System.String UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        System.String Password { get; set; }

        /// <summary>
        /// Gets or sets the LastName.
        /// </summary>
        /// <value>The password.</value>
        System.String LastName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        System.String OtherNames { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        UserRole Role { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        string Email { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        UserStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the last login date.
        /// </summary>
        /// <value>The last login date.</value>
        System.DateTime LastLoginDate { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>The creation date.</value>
        System.DateTime CreationDate { get; set; }
        System.DateTime PasswordExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating a first time login
        /// </summary>
        bool IsFirstLogin { get; set; }

        long? InstitutionID { get; set; }

        bool ApprovalRight { get; set; }
    }
}
