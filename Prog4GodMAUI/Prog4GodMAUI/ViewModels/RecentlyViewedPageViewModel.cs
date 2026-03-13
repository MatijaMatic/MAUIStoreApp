using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prog4GodMAUI.Services;
using Prog4GodMAUI.Models;
using Prog4GodMAUI.Views;

namespace Prog4GodMAUI.ViewModels
{
    public partial class RecentlyViewedPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public RecentlyViewedProductsService recentlyViewedProductsService;

        public RecentlyViewedPageViewModel(RecentlyViewedProductsService recentlyViewedProductsService)
        {
            RecentlyViewedProductsService = recentlyViewedProductsService;
        }

        public RecentlyViewedPageViewModel()
        {
        }
        [RelayCommand]
        public async Task Init()
        {
            RecentlyViewedProductsService.LoadProducts();

            await Task.CompletedTask;
        }

        [RelayCommand]
        private async Task ProductTapped(Product product)
        {
            IsBusy = true;

            if (product == null)
            {
                return;
            }

            var navigationParameter = new Dictionary<string, object>
            {
                { "Product", product },
            };

            await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}", true, navigationParameter);

            IsBusy = false;
        }

        [RelayCommand]
        private async Task GoToHome()
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        [RelayCommand]
        private async Task DeleteAll()
        {
            var result = await Shell.Current.DisplayAlert("Delete All", "Are you sure you want to delete all recently viewed products?", "Yes", "No");

            if (!result)
            {
                return;
            }

            RecentlyViewedProductsService.RecentlyViewedProducts.Clear();
            RecentlyViewedProductsService.SaveProducts();

            await Task.CompletedTask;
        }
    }
}
