using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustokk.BLL.Services.Contracts;
using Pustokk.BLL.UI.Services;
using Pustokk.BLL.ViewModels.BasketItemViewModels;
using Pustokk.DAL.DataContext;
using Pustokk.DAL.DataContext.Entities;
using Pustokk.MVC.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Pustokk.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        private readonly IProductService _productService;

        private readonly AppDbContext _appDbContext;

        private const string BASKET_KEY = "OGANI_BASKET_KEY";

        public HomeController(IHomeService homeService, IProductService productService,  AppDbContext appDbContext)
        {
            _homeService = homeService;
            _productService = productService;
            _appDbContext = appDbContext;
        }



        public async Task<IActionResult> Index(int? categoryId)
        {
            var viewModel = await _homeService.GetHomeViewModelAsync(categoryId);
            return View(viewModel);
        }

        //bax!!! islemedi deye try catch vermisen 
        public async Task<IActionResult> Details(int id)
        {

            try
            {
                var productDetails = await _productService.GetProductDetailsAsync(id);
                return View(productDetails);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }


        }

        public async Task<IActionResult> AddToBasket(int id, string? returnUrl)
        {
            var product = await _productService.GetAsync(id);

            if (product == null)
                return NotFound();

            //login olubsa database, olmayibsa cookies
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                string? json = Request.Cookies[BASKET_KEY];

                List<BasketItemViewModel> basket = new();
                //json null deyilse ora bir list atacaq, eger json null gelmirse basketi jsona deseralize edirsen
                if (!string.IsNullOrWhiteSpace(json))
                    basket = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(json!) ?? new();
                //json nulldisa
                var existItem = basket.FirstOrDefault(x => x.ProductId == id);

                if (existItem is { })
                    existItem.Count++;
                else
                {
                    BasketItemViewModel newItem = new()
                    {
                        ProductId = id,
                        Count = 1
                    };

                    basket.Add(newItem);
                }

                //mende hazir olan cookini jsona qaytarmaliyam

                var newJson = JsonConvert.SerializeObject(basket);

                //cookiye gonderdi
                Response.Cookies.Append(BASKET_KEY, newJson);
            }
            else
            {
                //login olunmus userin idsi getrir, token - claim- identifername-id
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var existItem = await _appDbContext.BasketItems.FirstOrDefaultAsync(x => x.ProductId == product.Id && x.AppUserId == userId);

                if (existItem is not null)
                {
                    existItem.Quantity++;
                    _appDbContext.Update(existItem);
                    await _appDbContext.SaveChangesAsync();

                    if (returnUrl is not null)
                        return Redirect(returnUrl);
                    return RedirectToAction("Index");
                }

                if (userId is null)
                    return BadRequest();

                //nulldisa yenisini yardacaq
                BasketItem basketItem = new()
                {
                    AppUserId = userId,
                    ProductId = product.Id,
                    Quantity = 1
                };

                await _appDbContext.BasketItems.AddAsync(basketItem);
                await _appDbContext.SaveChangesAsync();

            }
            //dinamik oldu redirect meseleem
            if (returnUrl is not null)
                return Redirect(returnUrl);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> ShoppingCard()
        {
            List<GetBasketViewModel> basket = new();

            //kecibse database
            if (User.Identity?.IsAuthenticated ?? false)
            {

                //tokenen istediyin valuenu getir
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId is null)
                    return BadRequest();
                //menim appuseridm olanlari getir null deyilse
                var basketItems2 = await _appDbContext.BasketItems.Include(x => x.Product).ThenInclude(x => x.ProductImages).Where(x => x.AppUserId == userId).ToListAsync();

                foreach (var basketItem in basketItems2)
                {
                    GetBasketViewModel vm = new()
                    {
                        Id = basketItem.Id,
                        Count = basketItem.Quantity,
                        Price = basketItem.Product.Price,
                        ImagePath = basketItem.Product.ProductImages?.FirstOrDefault()?.ImageUrl ?? "",
                        Name = basketItem.Product.Name,
                        ProductId = basketItem.Product.Id,
                    };
                    basket.Add(vm);

                }
                return View(basket);

            }


            //kecmeyibse
            List<BasketItemViewModel> basketItems = new();

            var json = Request.Cookies[BASKET_KEY];
            //nul deyilse json
            if (!string.IsNullOrEmpty(json))
                basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(json) ?? new();


            //json nulldisa
            foreach (var basketItem in basketItems)
            {
                var product = await _productService.GetAsync(basketItem.ProductId);

                if (product == null)
                    continue;
                GetBasketViewModel vm = new()
                {

                    Count = basketItem.Count,
                    Price = product.Price,
                    ImagePath = product.ImageUrls.FirstOrDefault() ?? "",
                    Name = product.Name,
                    ProductId = product.Id
                };
                basket.Add(vm);

            }
            return View(basket);
        }


		
		public async Task<IActionResult> DecrementBasketItem(int id)
		{
			if (!User.Identity?.IsAuthenticated ?? true)
			{
				// Cookie-based logic for non-authenticated users
				var json = Request.Cookies[BASKET_KEY];
				if (string.IsNullOrWhiteSpace(json))
					return BadRequest("Basket is empty");

				var basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(json) ?? new();

				var item = basketItems.FirstOrDefault(x => x.ProductId == id);
				if (item != null)
				{
					if (item.Count > 1)
						item.Count--;
					else
						basketItems.Remove(item);

					// Update cookie
					var newJson = JsonConvert.SerializeObject(basketItems);
					Response.Cookies.Append(BASKET_KEY, newJson);
					return Ok();
				}
			}
			else
			{
				// Database logic for authenticated users
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				if (userId == null)
					return BadRequest("User not found");

				var basketItem = await _appDbContext.BasketItems
					.FirstOrDefaultAsync(x => x.ProductId == id && x.AppUserId == userId);

				if (basketItem != null)
				{
					if (basketItem.Quantity > 1)
					{
						basketItem.Quantity--;
						_appDbContext.BasketItems.Update(basketItem);
					}
					else
					{
						_appDbContext.BasketItems.Remove(basketItem);
					}
					await _appDbContext.SaveChangesAsync();
					return Ok();
				}
			}

			return NotFound("Product not found in the basket");
		}
	}
}
