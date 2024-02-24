namespace Model.DTO
{
    public class UgovorDTO
    {
        #region Fields
        public int? BrojUgovora { get; set; }
        public DateTime? DatumOd { get; set; }
        public DateTime? DatumDo { get; set; }
        public KorisnikDTO Korisnik { get; set; }
        public PaketDTO Paket { get; set; }
        public List<PretplataDTO> Pretplate { get; set; }
        #endregion

        public UgovorDTO()
        {
            Korisnik = new KorisnikDTO();
            Paket = new PaketDTO();
            Pretplate = new List<PretplataDTO>();
        }
    }
}
