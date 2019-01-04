using System;
using System.Collections.Generic;
using System.Linq; 
using Smd.UI;
using Smd.Zero;
using Microsoft.AspNetCore.Identity;
using Smd.Collections.Extensions;

namespace Smd.IdentityFramework
{
    public static class IdentityResultExtensions
    {
        private static readonly Dictionary<string, string> IdentityLocalizations
            = new Dictionary<string, string>
              {
                  {"Optimistic concurrency failure, object has been modified.", "Identity.ConcurrencyFailure"},
                  {"An unknown failure has occurred.", "Identity.DefaultError"},
                  {"Email '{0}' is already taken.", "Identity.DuplicateEmail"},
                  {"Role name '{0}' is already taken.", "Identity.DuplicateRoleName"},
                  {"User name '{0}' is already taken.", "Identity.DuplicateUserName"},
                  {"Email '{0}' is invalid.", "Identity.InvalidEmail"},
                  {"The provided PasswordHasherCompatibilityMode is invalid.", "Identity.InvalidPasswordHasherCompatibilityMode"},
                  {"The iteration count must be a positive integer.", "Identity.InvalidPasswordHasherIterationCount"},
                  {"Role name '{0}' is invalid.", "Identity.InvalidRoleName"},
                  {"Invalid token.", "Identity.InvalidToken"},
                  {"User name '{0}' is invalid, can only contain letters or digits.", "Identity.InvalidUserName"},
                  {"A user with this login already exists.", "Identity.LoginAlreadyAssociated"},
                  {"Incorrect password.", "Identity.PasswordMismatch"},
                  {"Passwords must have at least one digit ('0'-'9').", "Identity.PasswordRequiresDigit"},
                  {"Passwords must have at least one lowercase ('a'-'z').", "Identity.PasswordRequiresLower"},
                  {"Passwords must have at least one non alphanumeric character.", "Identity.PasswordRequiresNonAlphanumeric"},
                  {"Passwords must have at least one uppercase ('A'-'Z').", "Identity.PasswordRequiresUpper"},
                  {"Passwords must be at least {0} characters.", "Identity.PasswordTooShort"},
                  {"Role {0} does not exist.", "Identity.RoleNotFound"},
                  {"User already has a password set.", "Identity.UserAlreadyHasPassword"},
                  {"User already in role '{0}'.", "Identity.UserAlreadyInRole"},
                  {"User is locked out.", "Identity.UserLockedOut"},
                  {"Lockout is not enabled for this user.", "Identity.UserLockoutNotEnabled"},
                  {"User {0} does not exist.", "Identity.UserNameNotFound"},
                  {"User is not in role '{0}'.", "Identity.UserNotInRole"}
              };

        /// <summary>
        /// Checks errors of given <see cref="IdentityResult"/> and throws <see cref="UserFriendlyException"/> if it's not succeeded.
        /// </summary>
        /// <param name="identityResult">Identity result to check</param>
        public static void CheckErrors(this IdentityResult identityResult)
        {
            if (identityResult.Succeeded)
            {
                return;
            }

            throw new UserFriendlyException(identityResult.Errors.Select(err => err.Description).JoinAsString(", "));
        }
 
 
    }
}