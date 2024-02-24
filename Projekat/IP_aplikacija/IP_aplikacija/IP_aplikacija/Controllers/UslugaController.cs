using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;
using System.Globalization;
using SystemOperations.UslugaSO;

namespace IP_aplikacija.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UslugaController : Controller
    {
        private IConfiguration _configuration;

        public UslugaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetUsluge")]
        public async Task<ActionResult<List<UslugaDTO>>> GetUsluge(
            [FromQuery] int sifra = 0,
            [FromQuery] string naziv = "",
            [FromQuery] int sifraCenovnika = 0,
            [FromQuery] DateTime? datumOd = null,
            [FromQuery] DateTime? datumDo = null,
            [FromQuery] decimal cena = 0
            )
        {
            Usluga usluga = new Usluga 
            { 
                Sifra = sifra
                , Naziv = naziv 
            };

            Cenovnik cenovnik = new Cenovnik
            {
                Sifra = sifraCenovnika,
                Usluga = usluga,
                DatumOd = datumOd ?? new DateTime(1753, 1, 1),
                DatumDo = datumDo,
                Cena = cena
            };

            usluga.Cenovnici.Add(cenovnik);

            GetUslugaSO systemOperation = new GetUslugaSO(_configuration);
            await systemOperation.ExecuteTemplate(usluga);

            List<UslugaDTO> usluge = Helpers.ConvertDTO.ConvertUslugeToUslugeDTO(systemOperation.Result);
            
            return Ok(usluge);
        }

        [HttpPost]
        [Route("AddUsluga")]
        public async Task<ActionResult> AddUsluga(UslugaDTO uslugaDTO)
        {
            Usluga usluga = Helpers.ConvertDTO.ConvertUslugaDTOToUsluga(uslugaDTO);
            AddUslugaSO so = new AddUslugaSO(_configuration);

            try
            {
                await so.ExecuteTemplate(usluga);
                return Ok("Uspešno uneta usluga.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Usluga nije uneta.");
            }
        }

        [HttpPost]
        [Route("UpdateUsluga")]
        public async Task<ActionResult> UpdateUsluga(UslugaDTO uslugaDTO)
        {
            Usluga usluga = Helpers.ConvertDTO.ConvertUslugaDTOToUsluga(uslugaDTO);
            UpdateUslugaSO so = new UpdateUslugaSO(_configuration);

            try
            {
                await so.ExecuteTemplate(usluga);
                return Ok("Uspešno ažurirana usluga.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Usluga nije ažurirana.");
            }
        }

        [HttpPost]
        [Route("DeleteUsluga")]
        public async Task<ActionResult> DeleteUsluga(UslugaDTO uslugaDTO)
        {
            Usluga usluga = Helpers.ConvertDTO.ConvertUslugaDTOToUsluga(uslugaDTO);
            DeleteUslugaSO so = new DeleteUslugaSO(_configuration);

            try
            {
                await so.ExecuteTemplate(usluga);
                return Ok("Uspešno obrisana usluga.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Usluga nije obrisana.");
            }
        }
    }
}
