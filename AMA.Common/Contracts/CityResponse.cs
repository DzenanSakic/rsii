namespace AMA.Common.Contracts
{
    public class CityResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CountryResponse Country { get; set; }
    }
}
