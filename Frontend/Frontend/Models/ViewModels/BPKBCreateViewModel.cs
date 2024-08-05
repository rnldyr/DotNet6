namespace Frontend.Models.ViewModels
{
    public class BPKBCreateViewModel
    {
        public TrBpkb TrBpkb { get; set; }
        public IEnumerable<MsStorageLocation> Locations { get; set; }
    }
}
