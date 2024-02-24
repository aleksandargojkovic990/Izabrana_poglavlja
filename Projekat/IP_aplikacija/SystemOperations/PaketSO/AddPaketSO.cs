using Microsoft.Extensions.Configuration;
using Model;

namespace SystemOperations.PaketSO
{
    public class AddPaketSO : SystemOperationBase
    {
        public AddPaketSO(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task ExecuteOperation(IEntity entity)
        {
            int id = await repository.GetNewId(new Paket());
            await repository.Save(entity);
            ((Paket)entity).Sifra = id;

            foreach (PaketUsluga paketUsluga in ((Paket)entity).PaketUsluge)
            {
                await repository.Save(paketUsluga);
            }
        }
    }
}