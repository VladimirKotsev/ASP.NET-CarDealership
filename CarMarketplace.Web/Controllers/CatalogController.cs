﻿namespace CarMarketplace.Web.Controllers
{
    using System.Security.Claims; 

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using CarMarketplace.Web.Controllers.Common;
    using CarMarketplace.Services.Contracts;
    using CarMarketplace.Web.ViewModels.Catalog;
    public class CatalogController : BaseController
    {
        private readonly ICatalogService catalogService;
        public CatalogController(ICatalogService _catalogService)
        {
            this.catalogService = _catalogService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Search(SearchViewModel model)
        {
            return View(await this.catalogService.GetSearchViewModelAsync(model));
        }

        [AllowAnonymous]
        [ActionName("Home")]
        public async Task<IActionResult> Home()
        {
            return View(await this.catalogService.GetLatestSalePostsAsync());
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Result(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View("Search", await this.catalogService.GetSearchViewModelAsync(model));
            }

            return View(await catalogService.GetFilteredSalePostsAsync(model));
        }
    }
}
