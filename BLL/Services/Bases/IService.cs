namespace BLL.Services.Bases
{
    public interface IService<TEntity, TModel>
        where TEntity : class, new() where TModel : class, new()
    {
        public IQueryable<TModel> Query();
        public Service Create(TEntity record);
        public Service Update(TEntity record);
        
        // I took this class from EZShop file but in my case,
        // my primary keys are Guids instead of integers because
        // I find it more convenient to use Guids as primary keys.
        public Service Delete(Guid id);
        
    }
}