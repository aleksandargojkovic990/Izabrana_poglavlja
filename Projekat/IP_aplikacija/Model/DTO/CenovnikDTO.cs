namespace Model.DTO
{
    public class CenovnikDTO
    {
        #region Fields
        public int? Sifra { get; set; }
        public UslugaDTO? Usluga { get; set; }
        public DateTime? DatumOd { get; set; }
        public DateTime? DatumDo { get; set; }
        public decimal? Cena { get; set; }
        #endregion
    }
}
