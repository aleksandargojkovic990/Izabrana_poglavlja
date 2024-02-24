using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.UgovorSO
{
    public class DeleteUgovorSO : SystemOperationBase
    {
        public DeleteUgovorSO(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task ExecuteOperation(IEntity entity)
        {
            List<Pretplata> pretplate = (await repository.Get(new Pretplata { Ugovor = (Ugovor)entity })).Cast<Pretplata>().ToList();

            foreach (Pretplata pretplata in pretplate)
            {
                await repository.Delete(pretplata);
            }

            await repository.Delete(entity);
        }
    }
}
