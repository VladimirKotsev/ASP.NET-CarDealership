﻿namespace CarMarketplace.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using CarMarketplace.Data;
    using CarMarketplace.Data.Models;
    using Contracts;
    using Mapping;
    using Web.ViewModels.Common;
    using CarMarketplace.Web.ViewModels.User;

    public class UserService : IUserService
    {
        private readonly CarMarketplaceDbContext dbContext;

        public UserService(CarMarketplaceDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task AddToUserFavouritesAsync(Guid postId, string userId)
        {
            var post = await this.dbContext
                .SalePosts
                .FirstAsync(sp => sp.Id == postId);

            var user = await this.dbContext
                .ApplicationUsers
                .FirstAsync(u => u.Id == Guid.Parse(userId));

            var userLikedPost = new SalePostApplicationUser()
            {
                SalePost = post,
                User = user,
                DateTimeAdded = DateTime.Now
            };

            user.Favorites.Add(userLikedPost);
            post.SalePostUsers.Add(userLikedPost);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<FavouriteViewModel>> GetUserFavouritesAsync(string userId)
        {
            ICollection<FavouriteViewModel> favSalePosts = new HashSet<FavouriteViewModel>();

            var userFavorites = await this.dbContext
                .ApplicationUsers
                .Where(u => u.Id == Guid.Parse(userId))
                .Select(a => a.Favorites)
                .FirstAsync();

            foreach (var favourite in userFavorites)
            {
                var salePost = await dbContext
                    .SalePosts
                    .Select(sp => new SalePostViewModel()
                    {
                        Car = new CarViewModel()
                        {
                            Make = AutoMapperConfig.MapperInstance.Map<CarManufacturerViewModel>(sp.Car.Manufacturer),
                            Model = AutoMapperConfig.MapperInstance.Map<CarModelViewModel>(sp.Car.Model),
                            Category = AutoMapperConfig.MapperInstance.Map<CategoryViewModel>(sp.Car.Category),
                            Color = AutoMapperConfig.MapperInstance.Map<ColorViewModel>(sp.Car.Color),
                            Description = sp.Car.Description,
                            TechnicalSpecificationURL = sp.Car.TechnicalSpecificationURL,
                            EuroStandart = sp.Car.EuroStandart,
                            Odometer = sp.Car.Odometer,
                            Province = AutoMapperConfig.MapperInstance.Map<ProvinceViewModel>(sp.Car.Province),
                            City = sp.Car.City,
                            VinNumber = sp.Car.VinNumber,
                            TransmissionType = sp.Car.TransmissionType,
                            Year = sp.Car.Year,
                            Engine = AutoMapperConfig.MapperInstance.Map<EngineViewModel>(sp.Car.Engine)
                        },
                        Seller = AutoMapperConfig.MapperInstance.Map<SellerViewModel>(sp.Car.Seller),
                        PublishDate = sp.PublishDate,
                        ImageUrls = sp.ImageUrls,
                        Price = sp.Price,
                        Id = sp.Id
                    })
                    .FirstAsync(s => s.Id == favourite.SalePostId);

                favSalePosts.Add(new FavouriteViewModel()
                {
                    SalePost = salePost,
                    DateTimeAdded = favourite.DateTimeAdded
                });
            }

            return favSalePosts;
        }

        public async Task<ICollection<Guid>> GetUserFavouritePostIdsAsync(string userId)
        {
            ICollection<Guid> postIds = new HashSet<Guid>();

            var userFavorites = await this.dbContext
                .ApplicationUsers
                .Where(u => u.Id == Guid.Parse(userId))
                .Select(a => a.Favorites)
                .FirstAsync();

            foreach (var id in userFavorites.Select(x => x.SalePostId))
            {
                postIds.Add(id);
            }

            return postIds;
        }

        public async Task RemoveUserFavouritePostAsync(Guid postId, string userId)
        {
            var post = await this.dbContext
                .SalePosts
                .FirstAsync(sp => sp.Id == postId);

            var user = await this.dbContext
                .ApplicationUsers
                .FirstAsync(u => u.Id == Guid.Parse(userId));

            var userLikedPost = new SalePostApplicationUser()
            {
                SalePost = post,
                User = user,
            };

            user.Favorites.Remove(userLikedPost);
            post.SalePostUsers.Remove(userLikedPost);
            this.dbContext.SalePostApplicationUsers.Remove(userLikedPost);

            await this.dbContext.SaveChangesAsync();
        }
    }
}
