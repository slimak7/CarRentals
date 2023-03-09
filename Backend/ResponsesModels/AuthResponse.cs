namespace Backend.ResponsesModels
{
    public class AuthResponse : BaseResponse
    {
        public string? userID { get; set; }
        public string? token { get; set; }
        public AuthResponse(List<string>? errors, bool success, string userID, string token) : base(errors, success)
        {
            this.userID = userID;
            this.token = token;
        }

        public AuthResponse(string errors) : base(new List<string> { errors }, false)
        {
        }
    }
}
