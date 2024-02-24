using Microsoft.Extensions.Configuration;
using Model;

namespace SystemOperations.CenovnikSO
{
    public class DeleteCenovnikSO : SystemOperationBase
    {
        public DeleteCenovnikSO(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task ExecuteOperation(IEntity entity)
        {
            await repository.Delete(entity);

            Usluga usluga = ((Cenovnik)entity).Usluga;
            List<Cenovnik> cenovnici = (await repository.Get(new Cenovnik { Usluga = usluga })).Cast<Cenovnik>().ToList();
            
            if (cenovnici.Count == 0)
            {
                List<PaketUsluga> paketUsluga = (await repository.Get(new PaketUsluga { Usluga = usluga })).Cast<PaketUsluga>().ToList();

                if (paketUsluga.Count > 0)
                {
                    foreach (PaketUsluga pu in paketUsluga)
                    {
                        await repository.Delete(pu);
                    }
                }

                await repository.Delete(usluga);
            }
        }
    }
}