using System.Collections.Generic;

namespace DomainModel
{
    public class Meal:BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Restaurant Restaurant { get; set; }
        public MealCategory MealCategory { get; set; }
        public List<MealContent> MealContents { get; set; }
        public MealType MealType { get; set; }
        public int LocalPriority { get; set; }
        public int GeneralPriority { get; set; }
    }
}