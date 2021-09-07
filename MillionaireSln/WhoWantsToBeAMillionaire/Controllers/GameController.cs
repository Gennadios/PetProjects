using Microsoft.AspNetCore.Mvc;
using WhoWantsToBeAMillionaire.Models.Services;

namespace WhoWantsToBeAMillionaire.Controllers
{
    public class GameController : Controller
    {
        private IGameService _gameService;

        public GameController(IGameService gameService) => _gameService = gameService;

        public IActionResult Index()
        {
            return View(_gameService);
        }
    }
}
