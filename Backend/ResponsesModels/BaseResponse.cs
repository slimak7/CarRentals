namespace Backend.ResponsesModels
{
    public class BaseResponse
    {
        public bool success { get; set; }
        public List<string>? errors { get; set; }

        protected BaseResponse(List<string> errors, bool success) 
        { 
            this.success = success;
            this.errors = errors;
        }
    }
}
