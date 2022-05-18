using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamariners.Core.Interface;

namespace Xamariners.Core.FakeData
{
    public class FakeIdentityUser : IUserIdentity
    {
        public FakeIdentityUser()
        {

        }
        public FakeIdentityUser(string userName)
        {
            this.UserName = userName;
        }

        public string Id { get; set; }

        public string UserName { get; set; }
    }
}
