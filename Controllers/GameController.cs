using CST350Milestone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CST350Milestone.Services;

namespace CST350Milestone.Controllers
{
    public class GameController : Controller
    {
        public static GameService game = new GameService();

        public GameController()
        {

        }

        public IActionResult Index()
        {
            return View("Index", game.board);
        }

        
        public IActionResult HandleButtonClick(string mine)
        {
            List<string> list = mine.Split(',').ToList<string>();
            int row = int.Parse(list[0]);
            int col = int.Parse(list[1]);
            game.board.floodFill(row, col);
            game.checkMove(row, col);

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

        public IActionResult ShowAllButtons(int buttonXCordinate, int buttonYCordinate) 
        {
            int row = buttonXCordinate;
            int col = buttonYCordinate;

            CellModel currentCell = game.board.Grid[row, col];

            return PartialView(currentCell);
        }

        public IActionResult doButtonRightClick(int buttonXCordinate, int buttonYCordinate)
        {
            //Run Any Code for the Right Click Here

            int row = buttonXCordinate;
            int col = buttonYCordinate;

            game.board.Grid[row, col].flagged = true;

            CellModel currentCell = game.board.Grid[row, col];

            return PartialView(currentCell);

        }

        public IActionResult ShowGameData() 
        {
            return PartialView(game);
        }
    }
}
