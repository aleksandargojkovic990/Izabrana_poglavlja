using Microsoft.Extensions.Configuration;
using Model;

namespace SystemOperations.UgovorSO
{
    public class AddUgovorSO : SystemOperationBase
    {
        public AddUgovorSO(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task ExecuteOperation(IEntity entity)
        {
            int id = await repository.GetNewId(new Ugovor());
            await repository.Save(entity);
            ((Ugovor)entity).BrojUgovora = id;

            foreach (Pretplata pretplata in ((Ugovor)entity).Pretplate)
            {
                await repository.Save(pretplata);
            }
        }
    }
}
