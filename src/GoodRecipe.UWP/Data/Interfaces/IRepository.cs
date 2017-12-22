using System.Threading.Tasks;

namespace GoodRecipe.UWP.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task LoadAll();
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
