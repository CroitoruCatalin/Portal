namespace Portal.Repositories
{
    public interface IRepositoryWrapper : IDisposable
    {
        IPostRepository Posts { get; }
        IUserRepository Users { get; }

        Task<int> SaveAsync();
    }
}
