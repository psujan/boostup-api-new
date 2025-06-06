﻿using AutoMapper;
using Boostup.API.Data;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Response;
using Boostup.API.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Boostup.API.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public virtual async Task<T?> Add(T t)
        {
           dbContext.Set<T>().Add(t);
           await dbContext.SaveChangesAsync();
           return t;
        }
        public virtual async Task<T?> Delete(int id)
        {
            try
            {
                var row = await dbContext.Set<T>().FindAsync(id);
                if (row == null)
                {
                    throw new Exception("Row Not Found");
                }
                dbContext.Set<T>().Remove(row);
                await dbContext.SaveChangesAsync();
                return row;
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.GetBaseException() as SqlException;

                if (sqlException != null)
                {
                    var number = sqlException.Number;

                    if (number == 547)
                    {
                        Console.WriteLine(ex.Message);
                        throw new Exception("Sorry! This record has other related records. This can't be deleted now.");
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
                throw new Exception("Exception Occured In Deleting Record" + ex.Message);
            }
        }
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        // Implement Update in Derived Class
        // To Handle ForeignKey and Other Properties Modification If Needed
        public virtual async Task<T?> Update(int id , T updatedT)
        {
           throw new NotImplementedException();
        }

        public virtual async Task<T?> GetById(int id)
        {
           return await dbContext.Set<T>().FindAsync(id);
        }
        public virtual async Task<PaginatedResponse<T>?> GetPaginated(int pageNumber , int pageSize)
        {

           var rows = await dbContext.Set<T>().OrderByDescending(x => EF.Property<object>(x, "Id")).Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
           var totalCount = await dbContext.Set<T>().CountAsync();
           var resultCount = rows.Count();
           return new PaginatedResponse<T>(rows, totalCount, resultCount, pageNumber, pageSize);

        }
    }
}
