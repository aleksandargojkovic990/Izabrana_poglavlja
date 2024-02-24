using Microsoft.Extensions.Configuration;
using Model;

namespace SystemOperations.SharedSO
{
    public class AddSO : SystemOperationBase
    {
        public AddSO(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task ExecuteOperation(IEntity entity)
        {
            await repository.Save(entity);
        }
    }
}
