using System.Collections.Generic;
using System;
using System.Linq;

namespace SalesWebMVC.Models {

    public class Department {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() { }

        public Department(int Id, string Name) {
            this.Id = Id;
            this.Name = Name;
        }

        public void AddSeller(Seller seller) {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime inital, DateTime final) {
            return Sellers.Sum(seller => seller.TotalSales(inital, final));
        }
    }
}
