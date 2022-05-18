using Xamariners.Core.Common.Enum;

namespace Xamariners.Core.Service.Interface.Requests
{
    public class AuthIdentifierRequest
    {
        public string AuthIdentifier { get; set; }
        public ContactType ContactType { get; set; }
    }
}