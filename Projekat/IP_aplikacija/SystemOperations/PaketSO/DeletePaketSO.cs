using Microsoft.Extensions.Configuration;
using Model;

namespace SystemOperations.PaketSO
{
    public class DeletePaketSO : SystemOperationBase
    {
        public DeletePaketSO(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task ExecuteOperation(IEntity entity)
        {
            foreach (PaketUsluga pu in ((Paket)entity).PaketUsluge)
            {
                await repository.Delete(pu);
            }

            await repository.Delete(entity);
        }
    }
}