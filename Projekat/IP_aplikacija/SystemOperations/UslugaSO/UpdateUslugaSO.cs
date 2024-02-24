using Microsoft.Extensions.Configuration;
using Model;

namespace SystemOperations.UslugaSO
{
    public class UpdateUslugaSO : SystemOperationBase
    {
        public UpdateUslugaSO(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task ExecuteOperation(IEntity entity)
        {
            await repository.Update(entity);

            foreach (Cenovnik cenovnik in ((Usluga)entity).Cenovnici)
            {
                await repository.Update(cenovnik);
            }
        }
    }
}
