﻿namespace CarMarketplace.Services.Contracts
{
    using CarMarketplace.Web.ViewModels.Catalog;

    public interface ICatalogService
    {
        public Task<ICollection<SalePostViewModel>> GetLatestSalePostsAsync();

        public Task<SearchViewModel> GetSearchViewModelAsync(SearchViewModel model);

        public Task<SalePostViewModel> GetSalePostByIdAsync(Guid postId);

        public Task<ICollection<CarModelViewModel>> GetModelsByMakeAsync(string make);

        public Task<ICollection<SalePostViewModel>> GetFilteredSalePostsAsync(SearchViewModel model);

        public Task<AddCarViewModel> GetAddPostViewModelAsync(AddCarViewModel viewModel);

        public Task AddPostAsync(AddCarViewModel viewModel, Guid sellerId);

        public Task DeletePostAsync(Guid postId);
    }
}
