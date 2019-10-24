
using Data.RepoInterface;
using DomainModel;
using System;

namespace Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRestaurantRepository RestaurantRepository { get; set; }
        IMealTypeRepository MealTypeRepository { get; set; }
        IMealCategoryRepository MealCategoryRepository { get; set; }

        int Complete();
    }
}