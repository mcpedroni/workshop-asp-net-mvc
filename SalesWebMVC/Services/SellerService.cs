using SalesWebMVC.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Services {
    public class SellerService {

        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context) {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync() {
            return await _context.Seller.OrderBy(s => s.Name).ToListAsync();
        }

        public async Task <Seller> FindByIdAsync(int id) {
            //return _context.Seller.FirstOrDefault(s => s.Id == id ); //this way search only seller

            //using EntityFrameworkCore to do join tables between Seller and Department
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task RemoveAsync(int id) {

            try {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            } catch (DbUpdateException e){
                throw new IntegrityException("Can not delete Seller because she/he has a sales");
            }

        }

        public async Task InsertAsync(Seller obj) {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj) {
            //Any: Is there any register in database? If not, throw a message
            var hasName = await _context.Seller.AnyAsync(x => x.Id == obj.Id);   
            if (!hasName) {
                throw new NotFoundException("Id not found");
            }

            //there is a register
            try {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException e){
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
