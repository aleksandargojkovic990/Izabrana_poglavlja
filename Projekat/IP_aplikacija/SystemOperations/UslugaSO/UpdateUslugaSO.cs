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
                if (cenovnik.Action == Model.Action.Update)
                    await repository.Update(cenovnik);
                else if(cenovnik.Action == Model.Action.Add)
                    await repository.Save(cenovnik);
                else if (cenovnik.Action == Model.Action.Delete)
                    await repository.Delete(cenovnik);
            }
        }
    }
}
