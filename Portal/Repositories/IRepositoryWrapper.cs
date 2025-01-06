namespace Portal.Repositories
{
    public interface IRepositoryWrapper : IDisposable
    {
        IPostRepository Posts { get; }

        Task<int> SaveAsync();
    }
}
