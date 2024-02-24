namespace Model.DTO
{
    public class UslugaDTO
    {
        #region Fields
        public int? Sifra { get; set; }
        public string Naziv { get; set; }
        public List<CenovnikDTO>? Cenovnici { get; set; }
        public bool isChecked { get; set; }
        #endregion

        public UslugaDTO()
        {
            Cenovnici = new List<CenovnikDTO>();
        }
    }
}
