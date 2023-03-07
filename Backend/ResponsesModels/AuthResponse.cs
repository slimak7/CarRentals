namespace Backend.ResponsesModels
{
    public class AuthResponse : BaseResponse
    {
        string? userID;
        string? token;
        public AuthResponse(List<string> errors, bool success, string userID, string token) : base(errors, success)
        {
            this.userID = userID;
            this.token = token;
        }

        public AuthResponse(string errors) : base(new List<string> { errors }, false)
        {
        }
    }
}
