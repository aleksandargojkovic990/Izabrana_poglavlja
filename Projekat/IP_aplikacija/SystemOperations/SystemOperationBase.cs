using Microsoft.Extensions.Configuration;
using Model;
using Storage;

namespace SystemOperations
{
    public abstract class SystemOperationBase
    {
        protected IGenericRepository repository;

        public SystemOperationBase(IConfiguration configuration)
        {
            repository = new GenericRepository(configuration);
        }

        public async Task ExecuteTemplate(IEntity entity)
        {
            try
            {
                repository.OpenConnection();
                repository.BeginTransaction();
                await ExecuteOperation(entity);
                repository.Commit();
            }
            catch (Exception)
            {
                repository.Rollback();
                throw;
            }
            finally
            {
                repository.CloseConnection();
            }
        }

        protected abstract Task ExecuteOperation(IEntity entity);
    }
}
