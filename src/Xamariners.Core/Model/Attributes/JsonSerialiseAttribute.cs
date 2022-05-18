using System;
using System.Collections.Generic;
using System.Linq;
using Xamariners.Core.Common.Enum;
using Xamariners.Core.Helpers;

namespace Xamariners.Core.Model.Attributes
{
    public class JsonSerialiseAttribute : Attribute
    {
        public JsonSerialiseAttribute(params UserRole[] allowedRoles)
        {
            if(allowedRoles.Contains(UserRole.None))
            {
                AllowedRoles = new List<UserRole>();
                return;
            }
            
            AllowedRoles = allowedRoles.ToList();

            if (!AllowedRoles.Contains(UserRole.Xamariners)) // && RoleHelper.IsXamarinersKing()
                AllowedRoles.Add(UserRole.Xamariners);
        }

        public List<UserRole> AllowedRoles { get; private set; }
    }
}