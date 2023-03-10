namespace Backend.ResponsesModels
{
    public class AuthResponse : BaseResponse
    {
        public string? userID { get; set; }
        public string? token { get; set; }
        public string? userType { get; set; }

        public string email { get; set; }
        public AuthResponse(List<string>? errors, bool success, string userID, string token, string? userType, string email) : base(errors, success)
        {
            this.userID = userID;
            this.token = token;
            this.userType = userType;
            this.email = email;
        }

        public AuthResponse(string errors) : base(new List<string> { errors }, false)
        {
        }
    }
}
