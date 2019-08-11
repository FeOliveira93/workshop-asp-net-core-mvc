using System;
using System.Linq;
using System.Collections.Generic;

namespace SalesWebMCV.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department()
        { 
        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public Double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(Seller => Seller.TotalSales(initial, final));
        }
    }
}
