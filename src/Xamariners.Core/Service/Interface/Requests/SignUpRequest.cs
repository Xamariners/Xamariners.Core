namespace Xamariners.Core.Service.Interface.Requests
{
    public class SignUpRequest
    {
        public string MobileNumber { get; set; }
        public string SmsCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}