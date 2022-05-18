using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamariners.Core.Common.Enum;
using Xamariners.Core.Interface;

namespace Xamariners.Core.FakeData
{
    public class FakeIdentityProvider : IIdentityProvider
    {
        public IUserIdentity GetNewInstance(string username)
        {
            return new FakeIdentityUser(username);
        }

        public IUserIdentity CreateUser(IUserIdentity user, string password)
        {
            return user;
        }

        public IUserRole GetRoleByName(UserRole userRole)
        {
            return default(IUserRole);
        }

        public void AddUserToRole(string userId, IUserRole roleName)
        {
            ;
        }

        public IUserIdentity GetIdentityByName(string username)
        {
            return default(IUserIdentity);
        }

        public void Delete(IUserIdentity identity)
        {
            ;
        }

        public void ChangePassword(string id, string password, string newPassword)
        {
            ;
        }

        public void RemoveUserFromRole(string id, IUserRole oldRoleIdentity)
        {
            ;
        }

        public IUserIdentity GetIdentityById(string id)
        {
            return default(IUserIdentity);
        }

        public IUserIdentity UpdateUser(IUserIdentity identity)
        {
            return default(IUserIdentity);
        }
    }
}
