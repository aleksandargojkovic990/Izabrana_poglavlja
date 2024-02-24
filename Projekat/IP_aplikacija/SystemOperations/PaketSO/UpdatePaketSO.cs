using Microsoft.Extensions.Configuration;
using Model;

namespace SystemOperations.PaketSO
{
    public class UpdatePaketSO : SystemOperationBase
    {
        public UpdatePaketSO(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task ExecuteOperation(IEntity entity)
        {
            await repository.Update(entity);

            foreach (PaketUsluga pu in ((Paket)entity).PaketUsluge)
            {
                await repository.Update(pu);
            }
        }
    }
}