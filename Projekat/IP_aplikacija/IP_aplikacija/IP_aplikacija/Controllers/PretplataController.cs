using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;
using SystemOperations.SharedSO;

namespace IP_aplikacija.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PretplataController : Controller
    {
        private IConfiguration _configuration;

        public PretplataController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetPretplate")]
        public async Task<ActionResult<List<PretplataDTO>>> GetPretplate(
            [FromQuery] bool jeAktivna,
            [FromQuery] int sifraUsluge = 0,
            [FromQuery] int brojUgovora = 0
            )
        {
            Usluga usluga = new Usluga { Sifra = sifraUsluge };
            Ugovor ugovor = new Ugovor { BrojUgovora = brojUgovora };
            Pretplata pretplata = new Pretplata
            {
                Usluga = usluga,
                Ugovor = ugovor,
                JeAktivna = jeAktivna
            };

            GetSO<Pretplata> systemOperation = new GetSO<Pretplata>(_configuration);
            await systemOperation.ExecuteTemplate(pretplata);

            List<PretplataDTO> pretplate = Helpers.ConvertDTO.ConvertPretplateToPretplateDTO(systemOperation.Result);

            return Ok(pretplate);
        }
    }
}
