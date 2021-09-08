using Microsoft.EntityFrameworkCore;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
	public interface IGenericService<TEntity, TModel> where TEntity : class, new() where TModel : class, new()
	{
		DbSet<TEntity> Get();
		Task<TModel> GetByIdAsync(Guid id);
		Task<Response<TModel>> UpdateAsync(TEntity entity);
		Task<Response<TModel>> AddAsync(TEntity entity);
		Task<int> DeleteByIdAsync(Guid id);
		Task<List<TModel>> GetAll();


	}
}
