using DatabaseBroker;
using Microsoft.Extensions.Configuration;
using Model;

namespace Storage
{
    public class GenericRepository : IGenericRepository
    {
        private Broker broker;

        public GenericRepository(IConfiguration configuration)
        {
            broker = new Broker(configuration);
        }

        public void OpenConnection()
        {
            broker.OpenConnection();
        }

        public void CloseConnection()
        {
            broker.CloseConnection();
        }

        public void BeginTransaction()
        {
            broker.BeginTransaction();
        }

        public void Commit()
        {
            broker.Commit();
        }

        public void Rollback()
        {
            broker.Rollback();
        }

        public async Task<List<IEntity>> Get(IEntity entity)
        {
            return await broker.Get(entity);
        }

        public async Task<int> GetNewId(IEntity e)
        {
            return await broker.GetNewId(e);
        }

        public async Task Save(IEntity entity)
        {
            await broker.Save(entity);
        }

        public async Task Update(IEntity e)
        {
            await broker.Update(e);
        }

        public async Task Delete(IEntity e)
        {
            await broker.Delete(e);
        }
    }
}