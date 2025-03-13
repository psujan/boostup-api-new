using AutoMapper;
using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;
using Boostup.API.Interfaces;
using Boostup.API.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Boostup.API.Repositories
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public LeaveRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<Leave> AddLeave(LeaveRequest request)
        {
            var leave = new Leave()
            {
                EmployeeId = request.EmployeeId,
                LeaveTypeId = request.LeaveTypeId,
                From = request.From,
                To = request.To,
                IsPaidLeave = request.IsPaidLeave,
                ForSingleDay = request.ForSingleDay,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            dbContext.Leave.Add(leave);
            await dbContext.SaveChangesAsync();
            return leave;
        }

        public async Task<Leave?> RemoveLeave(int Id)
        {
            var leave = await dbContext.Leave.FindAsync(Id);
            if (leave == null)
            {
                throw new Exception("Leave Not Found");
            }
            dbContext.Leave.Remove(leave);
            await dbContext.SaveChangesAsync();
            return leave;
        }

        public async Task<IEnumerable<Leave>?> GetLeaveByEmployee(int EmployeeId)
        {
            var rows = await dbContext.Leave.Where(leave => leave.EmployeeId == EmployeeId).ToListAsync();
            return rows;
        }

        public async Task<PaginatedResponse<LeaveResponse?>> GetLeave(LeaveFilterRequest request)
        {
            IQueryable<Leave> query = dbContext.Set<Leave>();
            HashSet<int> leaveIds = new HashSet<int>(request.LeaveTypeIds ?? new int[] { 0 });
            HashSet<int> empIds = new HashSet<int>(request.EmployeeIds ?? new int[] { 0 });

            if (!request.IsAllEmployee)
            {
                query = query.Where(leave => empIds.Contains(leave.EmployeeId));
            }

            if (!request.IsAllLeaveType)
            {
                query = query.Where(l => leaveIds.Contains(l.LeaveTypeId != null ? (int)l.LeaveTypeId : -1));
            }

            if (request.Year != null)
            {
                query = query.Where(leave => leave.From.Year.ToString() == request.Year);
            }

            if (request.Status != null)
            {
                query = query.Where(leave => leave.Status == request.Status);
            }

            query = query.Include(l => l.LeaveType).Include(l => l.Employee).ThenInclude(emp => emp.User);
            var rows = await query.Skip((request.PageNumber - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .AsNoTracking().ToListAsync();
            var totalCount = await dbContext.Leave.CountAsync();
            var resultCount = rows.Count();
            var mappedData = mapper.Map<IEnumerable<LeaveResponse>>(rows);
            return new PaginatedResponse<LeaveResponse?>(mappedData, totalCount, resultCount, request.PageNumber, request.PageSize);
        }

        public async Task<LeaveResponse?> GetLeaveById(int Id)
        {
            var data = await dbContext.Leave
                            .Include(l => l.LeaveType)
                            .Include(l => l.Employee)
                            .ThenInclude(emp => emp.User)
                            .FirstOrDefaultAsync(x => x.Id == Id);

            if (data == null)
            {
                throw new Exception("Leave Request Not Found");
            }

            var mappedData = mapper.Map<LeaveResponse>(data);
            return mappedData;
        }

        public async Task<LeaveResponse?> UpdateLeave(int Id, LeaveUpdateRequest request)
        {
            var leave = await dbContext.Leave.FindAsync(Id);
            if (leave == null)
            {
                throw new Exception("Leave Request Not Found");
            }

            leave.ForSingleDay = request.ForSingleDay;
            leave.Status = request.Status;
            leave.EmployeeId = request.EmployeeId;
            leave.From = request.From;
            leave.To = request.To;
            leave.RejectReason = request.RejectReason;
            leave.IsPaidLeave = request.IsPaidLeave;
            leave.Notes = request.Notes;
            leave.LeaveTypeId = request.LeaveTypeId;
            await dbContext.SaveChangesAsync();
            return mapper.Map<LeaveResponse>(leave);
        }
    }
}
