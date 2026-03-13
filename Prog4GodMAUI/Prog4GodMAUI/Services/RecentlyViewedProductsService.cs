using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Prog4GodMAUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog4GodMAUI.Services
{
    public partial class RecentlyViewedProductsService : ObservableObject
    {
        public RecentlyViewedProductsService()
        {
        }

        [ObservableProperty]
        private Collection<Product> recentlyViewedProducts = new();

        public void AddProduct(Product product)
        {
            var existingProduct = RecentlyViewedProducts.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                RecentlyViewedProducts.Remove(existingProduct);
            }

            RecentlyViewedProducts.Insert(0, product);

            if (RecentlyViewedProducts.Count > 8)
            {
                RecentlyViewedProducts.RemoveAt(RecentlyViewedProducts.Count - 1);
            }

            SaveProducts();
        }

        public void LoadProducts()
        {
            var productsJson = Preferences.Get("recently_viewed", string.Empty);
            if (!string.IsNullOrEmpty(productsJson))
            {
                var products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(productsJson);
                RecentlyViewedProducts = products ?? new ObservableCollection<Product>();
            }
        }
        public void SaveProducts()
        {
            var productsJson = JsonConvert.SerializeObject(RecentlyViewedProducts);
            Preferences.Set("recently_viewed", productsJson);
        }
    }
}
