namespace Backend.ResponsesModels
{
    public class BaseResponse
    {
        protected bool success { get; set; }
        protected List<string> errors { get; set; }

        protected BaseResponse(List<string> errors, bool success) 
        { 
            this.success = success;
            this.errors = errors;
        }
    }
}
