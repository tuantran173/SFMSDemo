namespace SFMSSolution.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        // Constructor thành công (truyền data)
        public ApiResponse(T data, string message = "Request successful")
        {
            Success = true;
            Message = message;
            Data = data;
        }

        // Constructor thất bại (truyền lỗi)
        public ApiResponse(string message)
        {
            Success = false;
            Message = message;
            Data = default;
        }

        // Constructor toàn quyền kiểm soát
        public ApiResponse(bool success, string message, T? data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
