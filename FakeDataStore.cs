namespace CQRS_MEDIATR
{
  public class FakeDataStore
  {
    private static List<Product> _products;

    public FakeDataStore()
    {
      _products = new List<Product>() {
                new Product {Id = 1, Name = "Product One"},
                new Product {Id = 2, Name = "Product Two"},
                new Product {Id = 3, Name = "Product Three"},
                new Product {Id = 4, Name = "Product Four"},
            };
    }

    public async Task AddProduct(Product product)
    {
      _products.Add(product);
      await Task.CompletedTask;
    }

    public async Task<IEnumerable<Product>> GetAllProducts() => await Task.FromResult(_products);
    public async Task<Product> GetProductById(int id) => await Task.FromResult(_products.Single(p => p.Id == id));

    public async Task EventOccured(Product product, string evt)
    {
      _products.Single(p => p.Id == product.Id).Name = $"{product.Name} event: {evt}";
      await Task.CompletedTask;
    }
  }
}