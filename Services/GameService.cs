using CST350Milestone.Models;
using System.Diagnostics;

namespace CST350Milestone.Services
{
    public class GameService
    {
        public BoardModel board = new BoardModel(10);

        public string playerName;
        public string Difficulty;
        public string moveLabel = "";
        public string bombLabel = "";
        public string GameResult = "";
        public string timeLabel = "";

        Stopwatch watch = new Stopwatch();

        public GameService()
        {
            this.playerName = "Test";
            this.Difficulty = "VeryEasy";
           
            //Setup The Game Based on Difficulty
            switch (Difficulty)
            {
                case "VeryEasy":
                    board.setupLiveNeighbors(5);
                    board.calculateLiveNeighbors();
                    break;

                case "Easy":
                    board.setupLiveNeighbors(10);
                    board.calculateLiveNeighbors();
                    break;

                case "Medium":
                    board.setupLiveNeighbors(15);
                    board.calculateLiveNeighbors();
                    break;

                case "Hard":
                    board.setupLiveNeighbors(20);
                    board.calculateLiveNeighbors();
                    break;
            }

            this.bombLabel = "Bombs Remaining: " + board.bombs.ToString();

            watch.Start();
        }

        public void GameSetup(string difficulty)
        {
            this.playerName = "Test";
            this.Difficulty = difficulty;

            //Setup The Game Based on Difficulty
            switch (Difficulty)
            {
                case "VeryEasy":
                    board.setupLiveNeighbors(5);
                    board.calculateLiveNeighbors();
                    break;

                case "Easy":
                    board.setupLiveNeighbors(10);
                    board.calculateLiveNeighbors();
                    break;

                case "Medium":
                    board.setupLiveNeighbors(15);
                    board.calculateLiveNeighbors();
                    break;

                case "Hard":
                    board.setupLiveNeighbors(20);
                    board.calculateLiveNeighbors();
                    break;
            }

            this.bombLabel = "Bombs Remaining: " + board.bombs.ToString();

            watch.Start();
        }

        public void flagSquare(int row, int col) 
        {
            //If its not already flagged flag it
            if (board.Grid[row, col].flagged == false)
            {
                //Sets the Square Flagged to True
                board.Grid[row, col].flagged = true;

                //If the Square is a Bomb Subtract it from the Total Bomb Count
                if (board.Grid[row, col].Live == true)
                {
                    TimeSpan ts = watch.Elapsed;

                    this.timeLabel = String.Format("Time Elapsed:{0} Minutes:{1} Seconds", ts.Minutes, ts.Seconds);

                    this.board.bombs = board.bombs - 1;

                    this.bombLabel = "Bombs Remaining: " + board.bombs.ToString();
                }
                
            }

            //If it is flagged already, remove the flag
            else 
            {
                board.Grid[row, col].flagged = false;

                if (board.Grid[row, col].Live == true)
                {
                    TimeSpan ts = watch.Elapsed;

                    this.timeLabel = String.Format("Time Elapsed:{0} Minutes:{1} Seconds", ts.Minutes, ts.Seconds);

                    this.board.bombs = board.bombs + 1;

                    this.bombLabel = "Bombs Remaining: " + board.bombs.ToString();
                }
                
            }

        }

        public void checkMove(int row, int col)
        {

            if (!board.Grid[row, col].Live)
            {
                this.moveLabel = "Moves: " + board.moves.ToString();

                this.bombLabel = "Bombs Remaining: " + board.bombs.ToString();

                TimeSpan ts = watch.Elapsed;

                this.timeLabel = String.Format("Time Elapsed:{0} Minutes:{1} Seconds", ts.Minutes, ts.Seconds);

                if (board.moves == board.Grid.Length - board.totalBombs)
                {
                    victory();
                }
            }
            else
            {
                TimeSpan ts = watch.Elapsed;

                this.timeLabel = String.Format("Time Elapsed:{0} Minutes:{1} Seconds", ts.Minutes, ts.Seconds);

                this.moveLabel = board.moves.ToString();

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
                case "VeryEasy":
                    score += 10000;
                    score -= (int)ts.TotalSeconds * 100;
                    break;

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

        public void reset()
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
                    board.Grid[r, c].flagged = false;
                }
            }
        moveLabel = "";
        bombLabel = "";
        GameResult = "";
        timeLabel = "";
        }
    }
}
