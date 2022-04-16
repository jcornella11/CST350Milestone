using CST350Milestone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CST350Milestone.Services;
using Nancy.Json;
using Newtonsoft.Json;

namespace CST350Milestone.Controllers
{
    public class GameController : Controller
    {
        public static GameService game = new GameService();

        List<GameModel> allgames = new List<GameModel>();

        GameDAO games = new GameDAO();

        public GameController()
        {
            games = new GameDAO();
        }

        public IActionResult Index()
        {
            return View("Index", game.board);
        }

        public IActionResult ShowOneButton(int buttonXCordinate, int buttonYCordinate) 
        {
            int row = buttonXCordinate;
            int col = buttonYCordinate;

            game.board.floodFill(row, col);
            game.checkMove(row, col);

            CellModel currentCell = game.board.Grid[row, col];

            return PartialView(currentCell);
        }

        public IActionResult ShowAllButtons() 
        {
            return PartialView(game.board);
        }

        public IActionResult doButtonRightClick(int buttonXCordinate, int buttonYCordinate)
        {
            
            int row = buttonXCordinate;
            int col = buttonYCordinate;

            game.flagSquare(row, col);

            CellModel currentCell = game.board.Grid[row, col];

            return PartialView(currentCell);
        }

        public IActionResult ShowGameData() 
        {
            return PartialView(game);
        }

        public void NewGame(string difficulty)
        {
            game.reset();
            game.GameSetup(difficulty);
        }

        public IActionResult SaveGame() {

        DateTime gameTime = DateTime.Now;

        string firstName = "Johnathan";

        string lastName = "Cornella";

        string gameData = JsonConvert.SerializeObject(game.board);

        GameModel thisgame = new GameModel(1, gameTime, firstName, lastName, gameData);

        games.Insert(thisgame);

        this.allgames = games.AllGames();

        return PartialView(allgames);

        }
    }
}
