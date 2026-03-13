using System;
using System.Collections.Generic;
using System.Text;
using Prog4GodMAUI.Models;

namespace Prog4GodMAUI.Services
{
    public class CategoryService : BaseService
    {
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await GetAsync<IEnumerable<string>>("products/categories");

            var categoryList = new List<Category>();
            foreach (var category in categories)
            {
                categoryList.Add(new Category { Name = category });
            }

            return categoryList;
        }
    }
}
