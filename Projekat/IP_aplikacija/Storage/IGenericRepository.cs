using Model;

namespace Storage
{
    public interface IGenericRepository
    {
        void OpenConnection();
        void CloseConnection();
        void BeginTransaction();
        void Commit();
        void Rollback();
        Task<List<IEntity>> Get(IEntity e);
        Task<int> GetNewId(IEntity e);
        Task Save(IEntity entity);
        Task Update(IEntity e);
        Task Delete(IEntity e);
    }
}
