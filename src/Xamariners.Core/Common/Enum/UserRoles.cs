using System;
using System.Linq;
using System.Reflection;
using Xamariners.Core.Common.Helpers;

namespace Xamariners.Core.Common.Enum
{
    // TODO move the xamariners related user roles out of core
    /// <summary>
    ///     The user role.
    /// </summary>
    [Flags]
    public enum UserRole
    {
        /// <summary>
        ///     The anonymous user.
        /// </summary>
        None = 0,

        /// <summary>
        /// Xamariners
        /// </summary>
        Xamariners = 1 << 0,
      
        /// <summary>
        /// Consumer
        /// </summary>
        Consumer = 1 << 2,

        /// <summary>
        /// Unregistered
        /// </summary>
        Unregistered = 1 << 3,

        /// <summary>
        /// Unregistered
        /// </summary>
        [ParentUserRole(Xamariners)]
        System = 1 << 4,

        /// <summary>
        ///     The xamariners master admin.
        /// </summary>
        [ParentUserRole(Xamariners)]
        XamarinersMasterAdmin = 1 << 5,

        /// <summary>
        ///     The xamariners super admin.
        /// </summary>
        [ParentUserRole(Xamariners)]
        XamarinersAdmin = 1 << 6,

        /// <summary>
        ///     The xamariners staff.
        /// </summary>
        [ParentUserRole(Xamariners)]
        XamarinersSupport = 1 << 7,

        /// <summary>
        ///     The member.
        /// </summary>
        [ParentUserRole(Consumer)]
        Member = 1 << 12,

        /// <summary>
        ///     The temporary member used for send invitation to Xamariners
        /// </summary>
        [ParentUserRole(Consumer)]
        TemporaryMember = 1 << 13,

        /// <summary>
        ///     The Anonymous User
        /// </summary>
        [ParentUserRole(Consumer)]
        Anonymous = 1 << 14,

        /// <summary>
        ///     The Reset User
        /// </summary>
        [ParentUserRole(Consumer)]
        Reset = 1 << 15,

    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ParentUserRoleAttribute : Attribute
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ParentUserRoleAttribute"/> class.
        /// </summary>
        /// <param name="parentUserRole">
        /// </param>
        public ParentUserRoleAttribute(UserRole parentUserRole)
        {
            ParentUserRole = parentUserRole;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// </summary>
        public UserRole ParentUserRole { get; set; }

        #endregion
    }

    /// <summary>
    /// </summary>
    public static class UserRoleExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <param name="UserRole">
        /// </param>
        /// <returns>
        /// The <see cref="UserRole"/>.
        /// </returns>
        public static UserRole[] GetChildUserRoles(this UserRole UserRole)
        {
            Type type = UserRole.GetType();

            UserRole[] result =
                System.Enum.GetNames(type)
                    .Select(n => new { Name = n, Field = type.GetRuntimeField(n.ToString()) })
                    .Select(
                        nf =>
                        new
                            {
                                nf.Name,
                                nf.Field,
                                PTCAs =
                            nf.Field.GetCustomAttributes(typeof(ParentUserRoleAttribute), false) as
                            ParentUserRoleAttribute[]
                            })
                    .Where(fp => fp.PTCAs.Length > 0 && fp.PTCAs.First().ParentUserRole == UserRole)
                    .Select(fp =>  fp.Name.ToEnum<UserRole>())
                    .ToArray();
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="UserRole">
        /// </param>
        /// <returns>
        /// The <see cref="UserRole"/>.
        /// </returns>
        public static UserRole? GetParentUserRole(this UserRole UserRole)
        {
            UserRole? result = null;
            Type type = UserRole.GetType();
            FieldInfo field = type.GetRuntimeField(UserRole.ToString());
            var ptcas = field.GetCustomAttributes(typeof(ParentUserRoleAttribute), false) as ParentUserRoleAttribute[];
            if (ptcas.Length > 0)
            {
                result = ptcas.First().ParentUserRole;
            }

            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="UserRole">
        /// </param>
        /// <returns>
        /// The <see cref="UserRole"/>.
        /// </returns>
        public static UserRole[] GetUserRoleTree(this UserRole userRole)
        {
            return
                userRole.SelectDeep(_ => _.GetParentUserRole().GetValueOrDefault())
                    .Where(_ => _ != UserRole.None)
                    .Reverse()
                    .ToArray();
        }

        #endregion

        // TODO: Move to common

    }
}
