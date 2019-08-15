using SalesWebMCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMCV.Services
{
    public class SellerService
    {
        private readonly SalesWebMCVContext _context;

        public SellerService(SalesWebMCVContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }


    }
}
