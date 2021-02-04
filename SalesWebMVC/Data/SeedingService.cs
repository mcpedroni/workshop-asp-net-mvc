using System;
using System.Linq;
using SalesWebMVC.Models;
using SalesWebMVC.Models.Enums;

namespace SalesWebMVC.Data {
    public class SeedingService {

        private SalesWebMVCContext _context;

        public SeedingService(SalesWebMVCContext context) {
            _context = context;
        }

        public void Seed() {
            //verifica se existe algum registro na base
            if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any()) {
                return; //Database has been seeded
            }

            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Electronics");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department { Id = 5, Name = "Books"};

            Seller s1 = new Seller(1, "Bob Brown", "bob@gmail.com", new DateTime(1988, 07, 22), 1000.0, d1);
            Seller s2 = new Seller(2, "Maria Green", "maria@gmail.com", new DateTime(1984, 07, 22), 2000.0, d2);
            Seller s3 = new Seller(3, "Alex Grey", "alex@gmail.com", new DateTime(1980, 07, 22), 1500.0, d3);
            Seller s4 = new Seller(4, "Donald Blue", "donald@gmail.com", new DateTime(1975, 07, 22), 1200.0, d4);

            SalesRecord r1 = new SalesRecord(1, new DateTime(2021, 02, 05), 11000.0, SaleStatus.Billed, s1);
            SalesRecord r2 = new SalesRecord(2, new DateTime(2021, 02, 06), 5000.0, SaleStatus.Canceled, s2);
            SalesRecord r3 = new SalesRecord(3, new DateTime(2021, 02, 07), 3000.0, SaleStatus.Pending, s3);
            SalesRecord r4 = new SalesRecord(4, new DateTime(2021, 02, 08), 4000.0, SaleStatus.Billed, s4);

            //Add records on database using entity framework
            _context.Department.AddRange(d1, d2, d3, d4); //AddRange allow insert many records at the same time
            _context.Seller.AddRange(s1, s2, s3, s4);
            _context.SalesRecord.AddRange(r1, r2, r3, r4);

            _context.SaveChanges();

        }

    }
}
