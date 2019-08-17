using SalesWebMCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMCV.Services
{
    public class DepartmentsService
    {
        private readonly SalesWebMCVContext _context;

        public DepartmentsService(SalesWebMCVContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }

    }
}
