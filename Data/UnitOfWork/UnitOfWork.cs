

using Data.RepoInterface;
using Data.Repository;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            RestaurantRepository = new RestaurantRepository(context);
            MealCategoryRepository = new MealCategoryRepository(context);
            MealTypeRepository = new MealTypeRepository(context);
        }

        public IRestaurantRepository RestaurantRepository { get; set; }
        public IMealCategoryRepository MealCategoryRepository { get; set; }
        public IMealTypeRepository MealTypeRepository { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}