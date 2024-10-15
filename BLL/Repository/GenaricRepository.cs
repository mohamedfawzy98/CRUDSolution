using BLL.InterFace;
using DAL.Data.Context;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class GenaricRepository<TEntity, TKey> : IGenaricRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly ApplicationDbContext _context;

        public GenaricRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if(typeof(Player<int>) == typeof(BaseEntity<TKey>))
            {
                return await _context.Players.OrderBy(p => p.Rate).ToListAsync() as IEnumerable<TEntity>;
            }
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetAsync(TKey? Id)
        {
            if (typeof(Player<int>) == typeof(BaseEntity<TKey>))
            {
                return await _context.Players.Include(p => p.Countery).SingleOrDefaultAsync(p => p.Id == Id as int?) as TEntity;

            }
            return await _context.Set<TEntity>().FindAsync(Id);

        }
        public async Task<int> AddAsync(TEntity entity)
        {
           await _context.Set<TEntity>().AddAsync(entity);
            return await _context.SaveChangesAsync();
        }
        public int Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return _context.SaveChanges();
        }
        public int Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return _context.SaveChanges();
        }
    }
}
