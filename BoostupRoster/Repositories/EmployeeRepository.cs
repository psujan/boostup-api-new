using AutoMapper;
using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;
using Boostup.API.Interfaces;
using Boostup.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Boostup.API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IFileUploadService fileService;

        public EmployeeRepository(ApplicationDbContext dbContext, IMapper mapper, IFileUploadService fileService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.fileService = fileService;
        }
        public async Task<EmployeeDetailResponse?> AddEmployee(User user, string? phone)
        {
            var employee = new EmployeeDetail()
            {
                UserId = user.Id,
                Contact = phone,
                JoinedDate = DateOnly.FromDateTime(DateTime.Now),
            };

            await dbContext.EmployeeDetail.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            return mapper.Map<EmployeeDetailResponse>(employee);
        }

        public async Task<IEnumerable<EmployeeBasicResponse>?> GetAll()
        {
            var rows = await dbContext.EmployeeDetail.Include(emp => emp.User).OrderBy(e => e.User.FullName).ToListAsync();
            return mapper.Map<IEnumerable<EmployeeBasicResponse>?>(rows);
        }

        public async Task<EmployeeDetailResponse?> GetById(int id)
        {
            var employee = await dbContext.EmployeeDetail
                            .Include(employee => employee.User)
                            .Include(employee => employee.Image)
                            .FirstOrDefaultAsync(x => x.Id == id);
            return employee == null ? throw new Exception("Employee With " + id + " not found") : mapper.Map<EmployeeDetailResponse>(employee);
        }

        public async Task<EmployeeBasicResponse> GetEmployeeFromUserId(string userId)
        {
            var employee = await dbContext.EmployeeDetail.FirstOrDefaultAsync(x => x.UserId == userId);
            return employee == null ? throw new Exception("Employee With " + userId + " not found") : mapper.Map<EmployeeBasicResponse>(employee);
        }

        public async Task<PaginatedResponse<EmployeeDetailResponse?>> GetPaginated(int pageNumber, int pageSize)
        {
            var rows = dbContext.EmployeeDetail
                        .Include(employee => employee.User)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .AsNoTracking();
            var data = await rows.ToListAsync();
            var totalCount = await dbContext.EmployeeDetail.CountAsync();
            var resultCount = rows.Count();
            var mappedData = mapper.Map<IEnumerable<EmployeeDetailResponse>>(data);
            return new PaginatedResponse<EmployeeDetailResponse?>(mappedData, totalCount, resultCount, pageNumber, pageSize);
        }

        public async Task<EmployeeDetailResponse?> UpdateEmployee(EmployeeProfileUpdateRequest request)
        {
            var employee = await dbContext.EmployeeDetail.FindAsync(request.EmployeeId) ?? throw new Exception("Employee Not Found");
            employee.Address = request.Address;
            employee.Contact = request.Contact;
            employee.EmergencyContact = request.EmergencyContact;
            employee.EmergencyContactName = request.EmergencyContactName;
            employee.BirthCountry = request.BirthCountry;
            employee.DOB = request.DOB;
            employee.Gender = request.Gender;
            employee.IsTaxFree = request.IsTaxFree;
            employee.BankName = request.BankName;
            employee.AccountNumber = request.AccountNumber;
            employee.TFN = request.TFN;
            employee.ABN = request.ABN;
            employee.EmploymentType = request.EmploymentType;
            employee.Notes = request.Notes;
            employee.Status = request.Status;
            employee.UpdatedAt = DateTime.Now;
            await dbContext.SaveChangesAsync();
            return mapper.Map<EmployeeDetailResponse>(employee);
        }

      
        public async Task<EmployeeProfileImage?> UpdateProfileImage(EmployeeProfileImageRequest request)
        {
            var profileImage = await fileService.UploadFile(request.ImageFile, "EmployeeProfile", "ProfileImages");
            if (profileImage == null)
            {
                return null;   
            }

            // check existing image and delete if the profile image already exists
            var image = await dbContext.EmployeeProfileImage.Where(x => x.EmployeeId == request.EmployeeId).FirstOrDefaultAsync();
            if (image != null) 
            {
                fileService.DeleteFileIfExists(image.Path);
                dbContext.Remove(image);
                await dbContext.SaveChangesAsync();
                // delete file from file system aswell
            }

            var employeeImage = new EmployeeProfileImage()
            {
                EmployeeId = request.EmployeeId,
                Path = profileImage.Path,
                Size = profileImage.ByteSize,
                Name =  profileImage.FileName,
                OriginalName = profileImage.OriginalName,
                CreatedAt = DateTime.Now,
            };
            dbContext.EmployeeProfileImage.Add(employeeImage);
            await dbContext.SaveChangesAsync();
            return employeeImage;
        }
    }
}
