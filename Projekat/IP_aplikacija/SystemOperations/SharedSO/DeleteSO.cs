using Microsoft.Extensions.Configuration;
using Model;

namespace SystemOperations.SharedSO
{
    public class DeleteSO : SystemOperationBase
    {
        public DeleteSO(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task ExecuteOperation(IEntity entity)
        {            
            await repository.Delete(entity);
        }
    }
}
