using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMCV.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Salary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double salary, Department department)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            BirthDate = birthDate;
            Salary = salary;
            Department = department ?? throw new ArgumentNullException(nameof(department));
        }

        public void AddSales(SalesRecord salesRecord)
        {
            Sales.Add(salesRecord);
        }

        public void RemoveSale(SalesRecord salesRecord)
        {
            Sales.Remove(salesRecord);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(x => x.Date >= initial && x.Date <= final).Sum(x => x.Amount);            
        }
    }
}
