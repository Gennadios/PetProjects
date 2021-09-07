using Microsoft.AspNetCore.Mvc;
using WhoWantsToBeAMillionaire.Models;
using WhoWantsToBeAMillionaire.Models.Services;

namespace WhoWantsToBeAMillionaire.Controllers
{
    public class HomeController : Controller
    {
        private IGameService _gameService;

        public HomeController(IGameService gameService) => _gameService = gameService;

        public IActionResult Index()
        {
            return View(_gameService);
        }
    }
}
