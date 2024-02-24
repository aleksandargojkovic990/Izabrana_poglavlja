using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;
using System.Globalization;
using SystemOperations.PaketSO;
using SystemOperations.UgovorSO;

namespace IP_aplikacija.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UgovorController : Controller
    {
        private IConfiguration _configuration;

        public UgovorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetUgovori")]
        public async Task<ActionResult<List<UgovorDTO>>> GetUgovori(
            [FromQuery] int brojUgovora = 0,
            [FromQuery] DateTime? datumOd = null,
            [FromQuery] DateTime? datumDo = null,
            [FromQuery] int sifraKorisnika = 0,
            [FromQuery] int sifraPaketa = 0
            )
        {
            Ugovor ugovor = new Ugovor
            {
                BrojUgovora = brojUgovora,
                DatumOd = datumOd ?? new DateTime(1753, 1, 1),
                DatumDo = datumDo ?? new DateTime(1753, 1, 1),
                Korisnik = new Korisnik
                {
                    Sifra = sifraKorisnika
                },
                Paket = new Paket
                {
                    Sifra = sifraPaketa
                }
            };

            GetUgovor systemOperation = new GetUgovor(_configuration);
            await systemOperation.ExecuteTemplate(ugovor);

            List<UgovorDTO> ugovori = Helpers.ConvertDTO.ConvertUgovoriToUgovoriDTO(systemOperation.Result);
            
            return Ok(ugovori);
        }

        [HttpPost]
        [Route("AddUgovor")]
        public async Task<ActionResult> AddUgovor(UgovorDTO ugovorDTO)
        {
            Ugovor ugovor = Helpers.ConvertDTO.ConvertUgovorDTOToUgovor(ugovorDTO);
            AddUgovorSO so = new AddUgovorSO(_configuration);
            try
            {
                await so.ExecuteTemplate(ugovor);
                return Ok("Uspešno unet ugovor.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ugovor nije unet.");
            }
        }

        [HttpPost]
        [Route("UpdateUgovor")]
        public async Task<ActionResult> UpdateUgovor(UgovorDTO ugovorDTO)
        {
            Ugovor ugovor = Helpers.ConvertDTO.ConvertUgovorDTOToUgovor(ugovorDTO);
            UpdateUgovorSO so = new UpdateUgovorSO(_configuration);

            try
            {
                await so.ExecuteTemplate(ugovor);
                return Ok("Uspešno ažuriran ugovor.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ugovor nije ažuriran.");
            }
        }

        [HttpPost]
        [Route("DeleteUgovor")]
        public async Task<ActionResult> DeleteUgovor(UgovorDTO ugovorDTO)
        {
            Ugovor ugovor = Helpers.ConvertDTO.ConvertUgovorDTOToUgovor(ugovorDTO);
            DeleteUgovorSO so = new DeleteUgovorSO(_configuration);

            try
            {
                await so.ExecuteTemplate(ugovor);
                return Ok("Uspešno obrisan ugovor.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ugovor nije obrisan.");
            }
        }
    }
}
