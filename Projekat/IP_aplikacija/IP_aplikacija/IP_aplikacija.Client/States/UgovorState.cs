using Model.DTO;

namespace IP_aplikacija.Client.States
{
    public class UgovorState
    {
        private UgovorDTO ugovor { get; set; }

        public bool IsSearch { get; set; } = false;
        public bool IsUpdate { get; set; } = false;

        public UgovorState()
        {
            if (ugovor is null)
                ugovor = new UgovorDTO();
        }

        public event Action OnStateChange;

        public void SetValue(UgovorDTO ugovor)
        {
            this.ugovor = ugovor;
            NotifyStateChanged();
        }

        public UgovorDTO GetValue()
        {
            return this.ugovor;
        }

        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
