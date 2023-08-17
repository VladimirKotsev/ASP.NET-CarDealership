﻿namespace CarMarketplace.Web.ViewModels.RentPosts
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    using Microsoft.AspNetCore.Http;

    using CarMarketplace.Web.ViewModels.Common;
    using static CarMarketplace.Common.EntityValidations.Car;
    using static CarMarketplace.Common.EntityValidations.CarModel;
    using static CarMarketplace.Common.EntityValidations.Engine;
    using static CarMarketplace.Common.EntityValidations.RentPost;
    public class AddRentPostViewModel
    {
        public ICollection<CarManufacturerViewModel>? Makes { get; set; }
        public ICollection<CategoryViewModel>? Categories { get; set; }

        [Required]
        public int MakeId { get; set; }

        [Required]
        [DisplayName("model")]
        [StringLength(ModelMaxLenght, MinimumLength = ModelMinLenght, ErrorMessage = ErrorMessage)]
        public string Model { get; set; } = null!;

        [DisplayName("year")]
        [Required]
        [Range(YearMinValue, YearMaxValue, ErrorMessage = ErrorMessage)]
        public int Year { get; set; }

        [DisplayName("displacement")]
        [Required]
        [Range(DisplacementMinValue, DisplacementMaxValue)]
        public int EngineDisplacement { get; set; }

        [DisplayName("fuel type")]
        [Required]
        public string EngineFuelType { get; set; } = null!;

        [DisplayName("horsepower")]
        [Required]
        [Range(HorsepowerMinValue, HorsepowerMaxValue)]
        public int EngineHorsePower { get; set; }

        [DisplayName("transmission")]
        [Required]
        public string TransmissionType { get; set; } = null!;

        [DisplayName("euro standart")]
        [Required]
        [Range(EuroStandartMinValue, EuroStandartMaxValue, ErrorMessage = ErrorMessage)]
        public int EuroStandart { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [DisplayName("price per day")]
        [Required]
        [Range(PricePerDayMaxValue, PricePerDayMinValue, ErrorMessage = ErrorMessage)]
        public int PricePerDay { get; set; }

        [Required]
        public IFormFile ImagePublicId { get; set; } = null!;

        public string? ImagesErrorMessage { get; set; }
    }
}
