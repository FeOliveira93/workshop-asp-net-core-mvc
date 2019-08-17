using SalesWebMCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMCV.Services
{
    public class DepartmentsService
    {
        private readonly SalesWebMCVContext _context;

        public DepartmentsService(SalesWebMCVContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
