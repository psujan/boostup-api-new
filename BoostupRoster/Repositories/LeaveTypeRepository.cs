using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Interfaces;

namespace Boostup.API.Repositories
{
    public class LeaveTypeRepository : BaseRepository<LeaveType> , ILeaveTypeRepository
    {
        public LeaveTypeRepository(ApplicationDbContext dbContenxt):base(dbContenxt)
        {
            
        }
    }
}
