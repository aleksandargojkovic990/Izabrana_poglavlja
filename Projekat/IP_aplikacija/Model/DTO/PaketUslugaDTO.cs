namespace Model.DTO
{
    public class PaketUslugaDTO
    {
        #region Fields
        public UslugaDTO? Usluga { get; set; }
        public PaketDTO? Paket { get; set; }
        public decimal Popust { get; set; }
        #endregion

        public PaketUslugaDTO()
        {
            Usluga = new UslugaDTO();
            Paket = new PaketDTO();
        }
    }
}
