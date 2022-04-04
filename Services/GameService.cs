using CST350Milestone.Models;
using System.Diagnostics;

namespace CST350Milestone.Services
{
    public class GameService
    {
        public BoardModel board = new BoardModel(10);

        int bombs;

        public string playerName;
        public string Difficulty;
        public string moves = "";
        public string bombLabel = "";
        public string GameResult = "";

        Stopwatch watch = new Stopwatch();

        public GameService()
        {
            this.playerName = "Test";
            this.Difficulty = "Medium";
            bombs = (int)board.bombs;

            switch (Difficulty)
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

            watch.Start();
        }

        public void checkMove(int row, int col)
        {

            if (!board.Grid[row, col].Live)
            {
                this.moves = "Moves: " + board.moves.ToString();

                this.bombLabel = "Bombs Remaining: " + board.bombs.ToString();

                if (board.moves == board.Grid.Length - board.bombs)
                {
                    victory();
                }
            }
            else
            {

                this.moves = board.moves.ToString();

                this.bombLabel = board.bombs.ToString();

                loss();

            }
        }

        private void victory()
        {
            watch.Stop();
            TimeSpan ts = watch.Elapsed;
            this.GameResult = string.Format("You Win!\nTime Elapsed: {0}:{1}\nScore: {2}", ts.Minutes, ts.Seconds, calculateScore(ts), "You Win!");
        }

        private void loss()
        {
            watch.Stop();
            this.GameResult = "You Lose and Your Score is 0";
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
