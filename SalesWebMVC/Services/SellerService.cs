using SalesWebMVC.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Services {
    public class SellerService {

        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context) {
            _context = context;
        }

        public List<Seller> FindAll() {
            return _context.Seller.OrderBy(s => s.Name).ToList();
        }

        public Seller FindById(int id) {
            //return _context.Seller.FirstOrDefault(s => s.Id == id ); //this way search only seller

            //using EntityFrameworkCore to do join tables between Seller and Department
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(s => s.Id == id);
        }

        public void Remove(int id) {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Insert(Seller obj) {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public void Update(Seller obj) {
            //Any: Is there any register in database? If not, throw a message
            if (!_context.Seller.Any(x => x.Id == obj.Id)) {
                throw new NotFoundException("Id not found");
            }

            //there is a register
            try {
                _context.Update(obj);
                _context.SaveChanges();
            } catch (DbUpdateConcurrencyException e){
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
