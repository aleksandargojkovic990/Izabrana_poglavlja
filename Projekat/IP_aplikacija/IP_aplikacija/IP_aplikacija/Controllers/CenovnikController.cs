using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model;
using System.Globalization;
using SystemOperations.CenovnikSO;
using SystemOperations.SharedSO;

namespace IP_aplikacija.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CenovnikController : ControllerBase
    {
        private IConfiguration _configuration;

        public CenovnikController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetCenovnici")]
        public async Task<ActionResult<List<CenovnikDTO>>> GetCenovnici(
            [FromQuery] int sifraCenovnika = 0,
            [FromQuery] int sifraUsluge = 0,
            [FromQuery] string nazivUsluge = "",
            [FromQuery] DateTime? datumOd = null,
            [FromQuery] DateTime? datumDo = null,
            [FromQuery] decimal cena = 0
            )
        {
            Usluga usluga = new Usluga() { Sifra = sifraUsluge, Naziv = nazivUsluge };
            Cenovnik cenovnik = new Cenovnik()
            {
                Sifra = sifraCenovnika,
                Usluga = usluga,
                DatumOd = datumOd ?? new DateTime(1753, 1, 1),
                DatumDo = datumDo,
                Cena = cena
            };

            GetCenovniciSO systemOperation = new GetCenovniciSO(_configuration);
            await systemOperation.ExecuteTemplate(cenovnik);

            List<CenovnikDTO> cenovnici = Helpers.ConvertDTO.ConvertCenovniciToCenovniciDTO(systemOperation.Result);
            
            return Ok(cenovnici);
        }

        [HttpPost]
        [Route("AddCenovnik")]
        public async Task<ActionResult> AddCenovnik(CenovnikDTO cenovnikDTO)
        {
            Cenovnik cenovnik = Helpers.ConvertDTO.ConvertCenovnikDTOToCenovnik(cenovnikDTO);
            AddSO so = new AddSO(_configuration);

            try
            {
                await so.ExecuteTemplate(cenovnik);
                return Ok("Uspešno unet cenovnik.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Cenovnik nije unet.");
            }
        }

        [HttpPost]
        [Route("UpdateCenovnik")]
        public async Task<ActionResult> UpdateCenovnik(CenovnikDTO cenovnikDTO)
        {
            Cenovnik cenovnik = Helpers.ConvertDTO.ConvertCenovnikDTOToCenovnik(cenovnikDTO);
            UpdateSO so = new UpdateSO(_configuration);

            try
            {
                await so.ExecuteTemplate(cenovnik);
                return Ok("Uspešno ažuriran cenovnik.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Cenovnik nije ažuriran.");
            }
        }

        [HttpPost]
        [Route("DeleteCenovnik")]
        public async Task<ActionResult> DeleteCenovnik(CenovnikDTO cenovnikDTO)
        {
            Cenovnik cenovnik = Helpers.ConvertDTO.ConvertCenovnikDTOToCenovnik(cenovnikDTO);
            DeleteCenovnikSO so = new DeleteCenovnikSO(_configuration);

            try
            {
                await so.ExecuteTemplate(cenovnik);
                return Ok("Uspešno obrisan cenovnik.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Cenovnik nije obrisan.");
            }
        }
    }
}
