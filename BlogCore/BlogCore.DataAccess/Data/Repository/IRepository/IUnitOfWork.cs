namespace BlogCore.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IArticleRepository Article { get; }
        ISliderRepository Slider { get; }
        IUserRepository User { get; }

        void Save();
    }
}
