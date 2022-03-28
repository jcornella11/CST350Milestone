using CST350Milestone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CST350Milestone.Controllers
{
    public class GameController : Controller
    {
        static BoardModel board = new BoardModel(10);

        int bombs = (int)board.bombs;

        public string playerName;
        public string Difficulty;

        Stopwatch watch = new Stopwatch();

        public GameController()
        {
            this.playerName = "Test";
            this.Difficulty = "Medium";
        }

        public IActionResult Index()
        {
            string difficulty = "Easy";

            switch (difficulty)
            {
                case "Easy":
                    board.setupLiveNeighbors(10);
                    board.calculateLiveNeighbors();
                    break;

                case "Medium":
                    board.setupLiveNeighbors(25);
                    board.calculateLiveNeighbors();
                    break;

                case "Hard":
                    board.setupLiveNeighbors(50);
                    board.calculateLiveNeighbors();
                    break;
            }

            string bombLabel = "Total Bombs on Board: " + bombs.ToString();

            ViewBag.bombLabel = bombLabel;

            watch.Start();

            return View("Index", board);
        }

        public IActionResult HandleButtonClick(string mine)
        {
            List<string> list = mine.Split(',').ToList<string>();
            int row = int.Parse(list[0]);
            int col = int.Parse(list[1]);
            board.floodFill(row, col);
            checkMove(row, col);

            return View("Index", board);
        }

        public IActionResult ShowOneButton(int buttonXCordinate, int buttonYCordinate)
        {
            int row = buttonXCordinate;
            int col = buttonYCordinate;

            board.floodFill(row, col);
            checkMove(row, col);

            CellModel currentCell = board.Grid[row, col];

            return PartialView(currentCell);
        }

        public IActionResult ShowAllButtons(int buttonXCordinate, int buttonYCordinate)
        {
            int row = buttonXCordinate;
            int col = buttonYCordinate;

            CellModel currentCell = board.Grid[row, col];

            return PartialView(currentCell);
        }

        public void checkMove(int row, int col)
        {

            if (!board.Grid[row, col].Live)
            {
                string moves = "Moves: " + board.moves.ToString();

                string bombLabel = "Bombs Remaining: " + board.bombs.ToString();

                ViewBag.bombLabel = bombLabel;

                ViewBag.moves = moves;

                ViewBag.timeElapsed = watch.Elapsed;

                if (board.moves == board.Grid.Length - board.bombs)
                {
                    ViewBag.GameResult = "You Win";
                    victory();

                }
            }
            else
            {

                string moves = board.moves.ToString();

                string bombLabel = board.bombs.ToString();

                watch.Stop();

                ViewBag.bombLabel = bombLabel;

                ViewBag.moves = moves;

                ViewBag.timeElapsed = watch.Elapsed;

                loss();

            }
        }

        private void victory()
        {
            watch.Stop();
            TimeSpan ts = watch.Elapsed;
            ViewBag.GameResult = string.Format("You Win!\nTime Elapsed: {0}:{1}\nScore: {2}", ts.Minutes, ts.Seconds, calculateScore(ts), "You Win!");
        }

        private void loss()
        {
            watch.Stop();
            ViewBag.GameResult = "You Lose and Your Score is 0";
        }

        public int calculateScore(TimeSpan ts)
        {
            int score = 0;

            switch (Difficulty)
            {
                case "Easy":
                    score += 10000;
                    score -= (int)ts.TotalSeconds * 100;
                    break;

                case "Medium":
                    score += 50000;
                    score -= (int)ts.TotalSeconds * 100;
                    break;

                case "Hard":
                    score += 100000;
                    score -= (int)ts.TotalSeconds * 100;
                    break;
            }
            if (score > 0)
            {
                return score;
            }
            return 0;
        }

        private void reset()
        {
            board.moves = 0;
            board.bombs = 0;
            watch.Reset();
            for (int r = 0; r < 10; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    board.Grid[r, c].LiveNeighbors = 0;
                    board.Grid[r, c].Live = false;
                    board.Grid[r, c].Visited = false;
                }
            }
        }
    }
}
