namespace GQL.Demo.Interfaces
{
    public interface IDataAccess
    {
        void Create(Product product);
        
        IList<Product> Get();
    }
}
