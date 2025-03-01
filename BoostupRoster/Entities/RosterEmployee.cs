namespace Boostup.API.Entities
{
    public class RosterEmployee
    {
        public int Id { get; set; } 
        public int RosterId { get; set; }
        public int EmployeeId { get; set; }
        public Roster Roster { get; set; }
        public EmployeeDetail Employee { get; set; }
    }
}
