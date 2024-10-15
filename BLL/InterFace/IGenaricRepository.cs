using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.InterFace
{
    public interface IGenaricRepository<TEntity , TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(TKey ? Id);
        Task<int> AddAsync(TEntity entity);
        int Update(TEntity entity);
        int Delete (TEntity entity);
    }
}
