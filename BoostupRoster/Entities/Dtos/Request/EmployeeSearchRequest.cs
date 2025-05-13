namespace Boostup.API.Entities.Dtos.Request
{
    public class EmployeeSearchRequest
    {
        public string? Search {  get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 50; // show 50 results at once

    }
}
