using Microsoft.Extensions.Configuration;
using Model;

namespace SystemOperations.SharedSO
{
    public class GetSO<T> : SystemOperationBase
    {
        public GetSO(IConfiguration configuration) : base(configuration)
        {
        }

        public List<T> Result { get; private set; }

        protected override async Task ExecuteOperation(IEntity entity)
        {
            Result = (await repository.Get(entity)).Cast<T>().ToList();
        }
    }
}
