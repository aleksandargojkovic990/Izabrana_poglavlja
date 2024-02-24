namespace Model.DTO
{
    public class PretplataDTO
    {
        #region Fields
        public UslugaDTO Usluga { get; set; }
        public UgovorDTO? Ugovor { get; set; }
        public bool JeAktivna { get; set; }
        #endregion

        public PretplataDTO()
        {
            Usluga = new UslugaDTO();
            Ugovor = new UgovorDTO();
        }
    }
}
