namespace SprinCTTest_Basvaraj.Models
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }
}
