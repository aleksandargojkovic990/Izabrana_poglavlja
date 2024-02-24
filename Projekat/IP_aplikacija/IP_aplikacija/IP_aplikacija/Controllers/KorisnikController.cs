using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;
using SystemOperations.SharedSO;

namespace IP_aplikacija.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {
        private IConfiguration _configuration;

        public KorisnikController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetKorisnici")]
        public async Task<ActionResult<List<KorisnikDTO>>> GetKorisnici(
            [FromQuery] int sifra = 0,
            [FromQuery] string jmbg = "",
            [FromQuery] string ime = "",
            [FromQuery] string prezime = "",
            [FromQuery] string brojTelefona = "",
            [FromQuery] string brojLicneKarte = "")
        {
            var korisnik = new Korisnik
            {
                Sifra = sifra,
                Jmbg = jmbg,
                Ime = ime,
                Prezime = prezime,
                BrojTelefona = brojTelefona,
                BrojLicneKarte = brojLicneKarte
            };

            GetSO<Korisnik> systemOperation = new GetSO<Korisnik>(_configuration);
            await systemOperation.ExecuteTemplate(korisnik);

            List<KorisnikDTO> korisnici = Helpers.ConvertDTO.ConvertKorisniciToKorisniciDTO(systemOperation.Result);

            return Ok(korisnici);
        }

        [HttpPost]
        [Route("AddKorisnik")]
        public async Task<ActionResult> AddKorisnik(KorisnikDTO korisnikDTO)
        {
            Korisnik korisnik = Helpers.ConvertDTO.ConvertKorisnikDTOToKorisnik(korisnikDTO);
            AddSO so = new AddSO(_configuration);

            try
            {
                await so.ExecuteTemplate(korisnik);
                return Ok("Uspešno unet korisnik.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Korisnik nije unet.");
            }
        }

        [HttpPost]
        [Route("UpdateKorisnik")]
        public async Task<ActionResult> UpdateKorisnik(KorisnikDTO korisnikDTO)
        {
            Korisnik korisnik = Helpers.ConvertDTO.ConvertKorisnikDTOToKorisnik(korisnikDTO);
            UpdateSO so = new UpdateSO(_configuration);

            try
            {
                await so.ExecuteTemplate(korisnik);
                return Ok("Uspešno ažuriran korisnik.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Korisnik nije ažuriran.");
            }
        }

        [HttpPost]
        [Route("DeleteKorisnik")]
        public async Task<ActionResult> DeleteKorisnik(KorisnikDTO korisnikDTO)
        {
            Korisnik korisnik = Helpers.ConvertDTO.ConvertKorisnikDTOToKorisnik(korisnikDTO);
            DeleteSO so = new DeleteSO(_configuration);

            try
            {
                await so.ExecuteTemplate(korisnik);
                return Ok("Uspešno obrisan korisnik.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Korisnik nije obrisan.");
            }
        }
    }
}
