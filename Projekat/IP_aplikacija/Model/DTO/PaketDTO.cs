namespace Model.DTO
{
    public class PaketDTO
    {
        #region Fields
        public int? Sifra { get; set; }
        public string Naziv { get; set; }
        public List<PaketUslugaDTO>? PaketUsluge { get; set; }
        #endregion

        public PaketDTO()
        {
            PaketUsluge = new List<PaketUslugaDTO>();
        }
    }
}
