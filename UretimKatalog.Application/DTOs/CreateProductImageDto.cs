using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace UretimKatalog.Application.DTOs
{
    public class CreateProductImageDto
    {
        public int ProductId { get; set; }
        public IFormFile File { get; set; } = null!;
        public bool IsMain { get; set; }
    }
}
