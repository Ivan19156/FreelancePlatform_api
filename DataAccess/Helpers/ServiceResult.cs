namespace DataAccess.Helpers
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        //public static ServiceResult Success(string message = "")
        //{
        //    return new ServiceResult { Success = true, Message = message };
        //}

        //public static ServiceResult Failure(string message)
        //{
        //    return new ServiceResult { Success = false, Message = message };
        //}
    }
}

