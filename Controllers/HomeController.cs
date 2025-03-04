using Microsoft.AspNetCore.Mvc;
using Rent_A_House_Amin_Mecha.DAL;
using Rent_A_House_Amin_Mecha.Models;
using Rent_A_House_Amin_Mecha.ViewModels;
using System.Diagnostics;

namespace Rent_A_House_Amin_Mecha.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IItemRepository _itemRepository;

        public HomeController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

       

        public async Task<IActionResult> Index()
        {
            var items = await _itemRepository.GetAll();
            var itemListViewModel = new ItemListViewModel(items, "Grid");
            return View(itemListViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
