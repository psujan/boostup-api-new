namespace Boostup.API.Entities.Dtos.Response
{
    public class JobResponseBasic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? JobAddress { get; set; }
        public string? Notes { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
    }
}
