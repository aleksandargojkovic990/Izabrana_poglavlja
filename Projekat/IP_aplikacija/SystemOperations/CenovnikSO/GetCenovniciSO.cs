using Microsoft.Extensions.Configuration;
using Model;

namespace SystemOperations.CenovnikSO
{

    public class GetCenovniciSO : SystemOperationBase
    {
        public GetCenovniciSO(IConfiguration configuration) : base(configuration)
        {
        }

        public List<Cenovnik> Result { get; private set; }

        protected override async Task ExecuteOperation(IEntity entity)
        {
            Result = (await repository.Get(entity)).Cast<Cenovnik>().ToList();
        }
    }
}
