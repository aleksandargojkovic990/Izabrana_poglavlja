using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOperations.UgovorSO
{
    public class GetUgovor : SystemOperationBase
    {
        public GetUgovor(IConfiguration configuration) : base(configuration)
        {
        }

        public List<Ugovor> Result { get; private set; }

        protected override async Task ExecuteOperation(IEntity entity)
        {
            Result = (await repository.Get(entity)).Cast<Ugovor>().ToList();

            Pretplata pretplata = ((Ugovor)entity).Pretplate.FirstOrDefault();
            if(pretplata is null)
            {
                pretplata = new Pretplata();
                pretplata.JeAktivna = true;
            }

            foreach (Ugovor ugovor in Result)
            {
                pretplata.Ugovor = ugovor;
                List<Pretplata> pretplate = (await repository.Get(pretplata)).Cast<Pretplata>().ToList();
                ugovor.Pretplate = pretplate;
            }
        }
    }
}
