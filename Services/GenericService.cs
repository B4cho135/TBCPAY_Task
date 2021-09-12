using AutoMapper;
using Core.Persistance;
using Microsoft.EntityFrameworkCore;
using Models.Responses;
using Models.Responses.Errors;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GenericService<TEntity, TModel> : IGenericService<TEntity, TModel> where TEntity : class, new() where TModel : class, new()
    {
		protected readonly DefaultDbContext Context;
		protected readonly IMapper Mapper;

		public GenericService(DefaultDbContext context, IMapper mapper)
		{
			Context = context;
			Mapper = mapper;
		}

		public async virtual Task<TModel> GetByIdAsync(int id)
		{
			TEntity entity = await Get().FindAsync(id);
			TModel model = Mapper.Map<TModel>(entity);
			return model;
		}
		public async Task<Response<TEntity>> AddAsync(TEntity entity)
		{
			try
			{
				await Context.AddAsync(entity);
				int status = await Context.SaveChangesAsync();


				//TModel model = Mapper.Map<TModel>(entity); //instead of returning TModel, TEntity is returned because it's easier to located newly creared person.

				var response = new Response<TEntity>()
				{
					Item = entity,
					StatusCode = status > 0 ? 201 : 400,
					Message = status == 0 ? "There has been a problem adding the entity to database!":"The entity has been succesfully added!",
					HasSucceeded = true
				};
				return response;
			}
			catch (Exception ex)
			{
				var response = new Response<TModel>()
				{
					Item = null,
					StatusCode = 500,
					Message = "Exception",
					Errors = new List<Error>()
					{
						new Error(ex.Message)
					}
				};
				return response;
			}
		}
		public async Task<int> DeleteByIdAsync(int id)
		{
			TModel model = await this.GetByIdAsync(id);
			TEntity entity = Mapper.Map<TEntity>(model);
			Context.Remove(entity);
			return await Context.SaveChangesAsync();
		}
		public DbSet<TEntity> Get()
		{
			return Context.Set<TEntity>();
		}
		public async virtual Task<List<TModel>> GetAll()
		{
			List<TEntity> entity = await Get().ToListAsync();
			List<TModel> model = Mapper.Map<List<TModel>>(entity);
			return model;
		}
		public async Task<Response<TModel>> UpdateAsync(TEntity entity)
		{
			try
			{
				Context.Update(entity);
				int status = await Context.SaveChangesAsync();
				TModel model = Mapper.Map<TModel>(entity);

				var response = new Response<TModel>()
				{
					Item = model,
					StatusCode = status > 0 ? 204 : 400,
					Message = status == 0 ? "There has been a problem updating the entity!":"The entity has been succesfully updated!",
					HasSucceeded = true
				};
				return response;
			}
			catch (Exception ex)
			{
				var response = new Response<TModel>()
				{
					Item = null,
					StatusCode = 500,
					Message = "Exception",
					Errors = new List<Error>()
					{
						new Error(ex.Message)
					}
				};
				return response;
			}
		}
	}
}
