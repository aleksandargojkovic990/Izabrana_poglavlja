using Microsoft.Extensions.Configuration;
using Model;

namespace SystemOperations.UslugaSO
{
    public class GetUslugaSO : SystemOperationBase
    {
        public GetUslugaSO(IConfiguration configuration) : base(configuration)
        {
        }

        public List<Usluga> Result { get; private set; }

        protected override async Task ExecuteOperation(IEntity entity)
        {
            Result = (await repository.Get(entity)).Cast<Usluga>().ToList();

            Cenovnik cenovnik = ((Usluga)entity).Cenovnici.First();
            foreach (Usluga usluga in Result)
            {
                cenovnik.Usluga = usluga;
                List<Cenovnik> cenovnici = (await repository.Get(cenovnik)).Cast<Cenovnik>().ToList();
                usluga.Cenovnici = cenovnici;
            }
        }
    }
}
