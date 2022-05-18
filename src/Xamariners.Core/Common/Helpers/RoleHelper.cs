using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamariners.Core.Common;
using Xamariners.Core.Common.Enum;

namespace Xamariners.Core.Helpers
{
    public static class RoleHelper
    {
        //public static bool IsXamarinersKing()
        //{
        //    return UserRole.Xamariners.IsInRoleRec(); //Xamariners is king
        //}

        //public static bool IsInRoles(this IEnumerable<UserRole> roles)
        //{
        //    var result = roles.Any(role => ServiceLocator.Current.GetInstance<ICredentialsProvider>().GetRoles().Contains(role));
        //    return result;
        //}

        //public static UserRole GetRoleInRoles(this IEnumerable<UserRole> roles)
        //{
        //    return roles.FirstOrDefault(role => ServiceLocator.Current.GetInstance<ICredentialsProvider>().GetRoles().Contains(role));
        //}

        //public static bool IsInRole(this UserRole role)
        //{
        //    return ServiceLocator.Current.GetInstance<ICredentialsProvider>().GetRoles().Contains(role);
        //}

        //public static bool IsInRoleRec(this UserRole role)
        //{
        //    return (role.IsInRole() || role.GetChildUserRoles().IsInRoles());
        //}

        public static UserRole GetRole(IEnumerable<UserRole> roles)
        {
            var returnRole = UserRole.None;
            if (roles.Count() > 1)
            {
                foreach (var role in roles)
                {
                    if (role.GetParentUserRole() != null)
                    {
                        returnRole = role;
                    }
                }
            }
            else
            {
                returnRole = roles.FirstOrDefault();
            }
            return returnRole;
        }

        public static bool IsXamarinersKing()
        {
            return false;
            //throw new NotImplementedException();
        }
    }
}
