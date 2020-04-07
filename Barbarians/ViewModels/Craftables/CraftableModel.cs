namespace Barbarians.ViewModels.Craftables
{
    public class CraftableModel
    {
        public string Title { get; set; }

        public string PartialName { get; set; }

        public CraftableModelForPartial PartialView { get; set; }
    }
}
