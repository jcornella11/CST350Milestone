using CST350Milestone.Models;
using CST350Milestone.Services;
using Microsoft.AspNetCore.Mvc;

namespace CST350Milestone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameAPIController : ControllerBase
    {
        public static GameDAO games = new GameDAO();

        public GameAPIController()
        {
            games = new GameDAO();
        }

        /*

        [HttpGet("showSavedGames")]
        public ActionResult <IEnumerable<GameModel>> Index()
        {
            return games.AllGames();
        }

        [HttpGet("showSavedGames/{Id}")]
        public ActionResult<GameModel> ShowOneGame(int Id) 
        {
            return games.GetGameById(Id);
        }

        [HttpPut("processinput")]
        public ActionResult<IEnumerable<GameModel>> GameInput(GameModel game) 
        {
            games.Insert(game);
            return games.AllGames();
        }
        */
    }
}
