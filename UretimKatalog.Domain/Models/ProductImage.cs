namespace UretimKatalog.Domain.Models
{
  public class ProductImage : BaseEntity
{
    public Guid ProductId { get; private set; }
    public string Url { get; private set; }
    public bool IsMain { get; private set; }

    public ProductImage(Guid productId, string url, bool isMain = false)
    {
        ProductId = productId;
        Url = url;
        IsMain = isMain;
    }
    public void SetMain() => IsMain = true;
}

}