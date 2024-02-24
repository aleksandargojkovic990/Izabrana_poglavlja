using Microsoft.Extensions.Configuration;
using Model;

namespace SystemOperations.SharedSO
{
    public class UpdateSO : SystemOperationBase
    {
        public UpdateSO(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task ExecuteOperation(IEntity entity)
        {
            await repository.Update(entity);
        }
    }
}
