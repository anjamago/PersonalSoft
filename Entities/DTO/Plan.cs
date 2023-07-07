namespace Entities.DTO
{
    public class Plan
    {
        public string? Id { get; set; }
        public string? PlanName { set; get; }
        public List<string>? ToppingsId { set; get; }
        public string? TotalCoverage { set; get; }
    }
}
