namespace API.DTOs
{
    public class RegionPutDto
    {
        public int RegionId { get; set; }
        public string? Name { get; set; }
        public int CityId { get; set; }

    }
}