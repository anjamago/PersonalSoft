using System.Net;

namespace Entities;

public class ResponseBase<T>
{

    public ResponseBase(HttpStatusCode code = HttpStatusCode.OK, string message = null, T data = default, int count = 0)
    {
        ResponseTime = DateTime.UtcNow.AddHours(-5);
        Code = (int)code;
        Message = message;
        Data = data;
        Count = count;
    }
    public string Message { get; set; }
    public int Count { get; set; }
    public DateTime ResponseTime { get; set; }
    public T Data { get; set; }
    public int Code { get; set; }
}
