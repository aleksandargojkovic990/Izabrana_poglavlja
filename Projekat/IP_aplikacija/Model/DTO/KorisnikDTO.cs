using System.ComponentModel.DataAnnotations;

namespace Model.DTO
{
    public class KorisnikDTO
    {
        #region Fields
        public int? Sifra { get; set; }
        [Required(ErrorMessage = "JMBG je obavezno polje.")]
        [StringLength(13, ErrorMessage = "JMBG treba da ima 13 karaktera.")]
        public string Jmbg { get; set; }
        [Required(ErrorMessage = "Ime je obavezno polje.")]
        [StringLength(50, ErrorMessage = "Ime može da ima max 50 karaktera.")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Prezime je obavezno polje.")]
        [StringLength(50, ErrorMessage = "Prezime može da ima max 50 karaktera.")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Broj telefona je obavezno polje.")]
        [StringLength(30, ErrorMessage = "Broj telefona može da ima max 30 karaktera.")]
        public string BrojTelefona { get; set; }
        [Required(ErrorMessage = "Broj lične karte je obavezno polje.")]
        [StringLength(9, ErrorMessage = "Broj lične karte treba da ima 9 karaktera.")]
        public string BrojLicneKarte { get; set; }
        #endregion
    }
}
