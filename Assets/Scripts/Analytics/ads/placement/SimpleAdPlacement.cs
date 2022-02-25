namespace Analytics.ads.placement
{
    public class SimpleAdPlacement: IAdPlacement
    {
        private readonly string name;

        public SimpleAdPlacement(string name) => this.name = name;

        public string GetName() => name;
    }
}