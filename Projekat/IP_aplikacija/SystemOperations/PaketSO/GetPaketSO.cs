using Microsoft.Extensions.Configuration;
using Model;

namespace SystemOperations.PaketSO
{
    public class GetPaketSO : SystemOperationBase
    {
        public GetPaketSO(IConfiguration configuration) : base(configuration)
        {
        }

        public List<Paket> Result { get; private set; }

        protected override async Task ExecuteOperation(IEntity entity)
        {
            Result = (await repository.Get(entity)).Cast<Paket>().ToList();

            foreach (Paket paket in Result)
            {
                List<PaketUsluga> paketUsluge = (await repository.Get(new PaketUsluga { Paket = paket })).Cast<PaketUsluga>().ToList();
                foreach (PaketUsluga pu in paketUsluge)
                {
                    pu.Usluga.Cenovnici.AddRange((await repository.Get(new Cenovnik { Usluga = pu.Usluga})).Cast<Cenovnik>().ToList());
                }
                paket.PaketUsluge = paketUsluge;
            }
        }
    }
}