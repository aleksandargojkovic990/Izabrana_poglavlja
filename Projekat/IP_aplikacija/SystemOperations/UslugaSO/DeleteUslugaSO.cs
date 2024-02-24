using Microsoft.Extensions.Configuration;
using Model;

namespace SystemOperations.UslugaSO
{
    public class DeleteUslugaSO : SystemOperationBase
    {
        public DeleteUslugaSO(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task ExecuteOperation(IEntity entity)
        {
            foreach (Cenovnik cenovnik in ((Usluga)entity).Cenovnici)
            {
                await repository.Delete(cenovnik);
            }
            
            await repository.Delete(entity);
        }
    }
}
