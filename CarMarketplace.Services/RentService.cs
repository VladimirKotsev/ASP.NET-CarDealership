﻿namespace CarMarketplace.Services
{
    using CarMarketplace.Data;
    using CarMarketplace.Services.Contracts;
    using CarMarketplace.Services.Mapping;
    using CarMarketplace.Web.ViewModels.Common;
    using CarMarketplace.Web.ViewModels.Lender;
    using CarMarketplace.Web.ViewModels.RentPosts;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class RentService : IRentService
    {
        private readonly CarMarketplaceDbContext dbContext;

        public RentService(CarMarketplaceDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<ICollection<RentPostViewModel>> GetRentPostViewModelAsync()
        {
            ICollection<RentPostViewModel> rentPosts = await this.dbContext
                .RentPosts
                .Select(rp => new RentPostViewModel()
                {
                    Car = new RentCarViewModel()
                    {
                        Make = AutoMapperConfig.MapperInstance.Map<CarManufacturerViewModel>(rp.Car.Manufacturer),
                        Model = AutoMapperConfig.MapperInstance.Map<CarModelViewModel>(rp.Car.Model),
                        Category = AutoMapperConfig.MapperInstance.Map<CategoryViewModel>(rp.Car.Category),
                        Color = AutoMapperConfig.MapperInstance.Map<ColorViewModel>(rp.Car.Color),
                        BootCapacity = rp.Car.BootCapacity,
                        Seats = rp.Car.Seats,
                        EuroStandart = rp.Car.EuroStandart,
                        TransmissionType = rp.Car.TransmissionType,
                        Year = rp.Car.Year,
                        Engine = AutoMapperConfig.MapperInstance.Map<EngineViewModel>(rp.Car.Engine)
                    },
                    Lender = new LenderViewModel()
                    {
                        Id = rp.LenderId,
                        PhoneNumber = rp.Lender.PhoneNumber,
                        CompanyName = rp.Lender.CompanyName,
                        City = new CityViewModel()
                        {
                            Province = AutoMapperConfig.MapperInstance.Map<ProvinceViewModel>(rp.Lender.City.Province),
                            CityName = rp.Lender.City.CityName,
                            CityId = rp.Lender.CityId
                        },
                        Address = rp.Lender.Address
                    },
                    ImagePublicId = rp.ImagePublicId,
                    PricePerDay = rp.PricePerDay,
                    Id = rp.Id,
                    IsRented = rp.IsRented
                })
                .ToArrayAsync();

            return rentPosts;
        }
    }
}
