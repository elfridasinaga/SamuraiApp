using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data.Interface;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Data
{
    public class SamuraiRepo : ISamurai
    {
        private readonly SamuraiContext _context;

        public SamuraiRepo(SamuraiContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            try
            {
                var deleteSamurai = await GetById(id);
                _context.Samurais.Remove(deleteSamurai);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbex)
            {
                throw new Exception(dbex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Samurai>> GetAll()
        {
            var results = await _context.Samurais.OrderBy(s => s.Name).AsNoTracking().ToListAsync();
            return results;
        }

        public async Task<Samurai> GetById(int id)
        {
            var result = await _context.Samurais.FirstOrDefaultAsync(s => s.Id == id);
            if (result == null) throw new Exception($"Data samurai id: {id} tidak ditemukan");
            return result;
        }

        public async Task<Samurai> Insert(Samurai obj)
        {
            try
            {
                _context.Samurais.Add(obj);
                _context.SaveChanges();
                return obj;
            }
            catch(DbUpdateConcurrencyException dbex)
            {
                throw new Exception(dbex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Samurai> Update(int id, Samurai obj)
        {
            try
            {
                var updateSamurai = await GetById(id);

                updateSamurai.Name = obj.Name;
                await _context.SaveChangesAsync();
                return updateSamurai;
            }
            catch(DbUpdateConcurrencyException dbex)
            {
                throw new Exception(dbex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
