using System.Net;

namespace UretimKatalog.Application.Bases
{
    public class BaseResponse<T>
{
    public bool Success           { get; set; } = true;
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    public T?   Data              { get; set; }
    public List<string>? Errors   { get; set; }
}

}
