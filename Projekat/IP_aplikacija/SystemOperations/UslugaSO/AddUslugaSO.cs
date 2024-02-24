using Microsoft.Extensions.Configuration;
using Model;

namespace SystemOperations.UslugaSO
{
    public class AddUslugaSO : SystemOperationBase
    {
        public AddUslugaSO(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task ExecuteOperation(IEntity entity)
        {
            int id = await repository.GetNewId(new Usluga());
            await repository.Save(entity);
            ((Usluga)entity).Sifra = id;

            foreach (Cenovnik cenovnik in ((Usluga)entity).Cenovnici)
            {
                await repository.Save(cenovnik);
            }
        }
    }
}
