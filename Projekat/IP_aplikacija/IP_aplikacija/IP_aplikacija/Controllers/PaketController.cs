using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;
using System.Globalization;
using SystemOperations.PaketSO;

namespace IP_aplikacija.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaketController : Controller
    {
        private IConfiguration _configuration;

        public PaketController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetPaketi")]
        public async Task<ActionResult<List<PaketUslugaDTO>>> GetPaketi(
            [FromQuery] int sifraPaketa = 0,
            [FromQuery] string nazivPaketa = "",
            [FromQuery] int sifraUsluge = 0,
            [FromQuery] decimal popust = 0)
        {
            Paket paket = new Paket 
            { 
                Sifra = sifraPaketa, 
                Naziv = nazivPaketa 
            };

            PaketUsluga paketUsluga = new PaketUsluga 
            { 
                Paket = paket,
                Usluga = new Usluga
                {
                    Sifra = sifraUsluge
                },
                Popust = popust
            };

            paket.PaketUsluge.Add(paketUsluga);

            GetPaketSO systemOperation = new GetPaketSO(_configuration);
            await systemOperation.ExecuteTemplate(paket);

            List<PaketDTO> paketi = Helpers.ConvertDTO.ConvertPaketiToPaketiDTO(systemOperation.Result);
            
            return Ok(paketi);
        }

        [HttpPost]
        [Route("AddPaket")]
        public async Task<ActionResult> AddPaket(PaketDTO paketDTO)
        {
            Paket paket = Helpers.ConvertDTO.ConvertPaketDTOToPaket(paketDTO);
            AddPaketSO so = new AddPaketSO(_configuration);
            try
            {
                await so.ExecuteTemplate(paket);
                return Ok("Uspešno unet paket.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Paket nije unet.");
            }
        }

        [HttpPost]
        [Route("UpdatePaket")]
        public async Task<ActionResult> UpdatePaket(PaketDTO paketDTO)
        {
            Paket paket = Helpers.ConvertDTO.ConvertPaketDTOToPaket(paketDTO);
            UpdatePaketSO so = new UpdatePaketSO(_configuration);

            try
            {
                await so.ExecuteTemplate(paket);
                return Ok("Uspešno ažuriran paket.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Paket nije ažuriran.");
            }
        }

        [HttpPost]
        [Route("DeletePaket")]
        public async Task<ActionResult> DeletePaket(PaketDTO paketDTO)
        {
            Paket paket = Helpers.ConvertDTO.ConvertPaketDTOToPaket(paketDTO);
            DeletePaketSO so = new DeletePaketSO(_configuration);

            try
            {
                await so.ExecuteTemplate(paket);
                return Ok("Uspešno obrisan paket.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Paket nije obrisan.");
            }
        }
    }
}
