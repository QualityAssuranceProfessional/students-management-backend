namespace API.DTOs
{
    public class RegionDto
    {
        private int cityId;

        public int RegionId { get; set; }
        public string Name { get; set; }
        public int CityId { get => cityId; set => cityId = value; }
        public DateTime? CreatedOn { get; set; }
    }
}
