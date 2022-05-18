using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamariners.Core.Common.Enum;
using Xamariners.Core.Model.Interface;

namespace Xamariners.Core.Interface
{
    public interface IIdentityProvider
    {
        IUserIdentity GetNewInstance(string username);
        IUserIdentity CreateUser(IUserIdentity user, string password);
        IUserRole GetRoleByName(UserRole userRole);
        void AddUserToRole(string userId, IUserRole roleName);
        IUserIdentity GetIdentityByName(string username);
        void Delete(IUserIdentity identity);
        void ChangePassword(string id, string password, string newPassword);
        void RemoveUserFromRole(string id, IUserRole oldRoleIdentity);
        IUserIdentity GetIdentityById(string id);
        IUserIdentity UpdateUser(IUserIdentity identity);
    }
}
