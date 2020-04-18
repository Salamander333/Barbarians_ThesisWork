namespace Barbarians.ViewModels.Healer
{
    public class HealerCharacterViewModel
    {
        public bool IsFullLife { get; set; }

        public int MissingHP { get; set; }

        public int CostToHeal => MissingHP * 2;
    }
}
