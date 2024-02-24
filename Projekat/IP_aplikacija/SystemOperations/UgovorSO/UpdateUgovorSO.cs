using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.UgovorSO
{
    public class UpdateUgovorSO : SystemOperationBase
    {
        public UpdateUgovorSO(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task ExecuteOperation(IEntity entity)
        {
            await repository.Update(entity);

            foreach (Pretplata pretplata in ((Ugovor)entity).Pretplate)
            {
                await repository.Update(pretplata);
            }
        }
    }
}