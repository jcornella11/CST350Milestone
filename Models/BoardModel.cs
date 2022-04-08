namespace CST350Milestone.Models
{
    public class BoardModel
    {
        public int Size;
        public CellModel[,] Grid;
        public decimal Difficulty;
        public int bombs;
        public int moves = 0;

        public BoardModel(int size)
        {
            Size = size;
            Grid = new CellModel[size, size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Grid[i, j] = new CellModel(i, j, false, false, 0);
                }
            }
        }

        public void setupLiveNeighbors(int difficulty)
        {
            // Check if difficulty percentage is between 1 and 100
            if (Enumerable.Range(1, 100).Contains(difficulty))
            {
                // Setting difficulty percentage and determining how many live tiles
                Difficulty = (decimal)difficulty / 100;
                decimal totalLive = Math.Round(Difficulty * Grid.Length);
                bombs = (int)totalLive;

                int nowLive = 0;
                Random random = new Random();

                // Setting random cells to live as long as they are currently dead.
                while (nowLive < totalLive)
                {
                    int row = random.Next(Size);
                    int col = random.Next(Size);

                    if (Grid[row, col].Live == false)
                    {
                        Grid[row, col].Live = true;
                        nowLive++;
                    }
                }
            }

            else
            {
                Console.WriteLine("Invalid difficulty percentage");
            }
        }

        public void calculateLiveNeighbors()
        {

            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    // Check if cell is live
                    if (Grid[row, col].Live == true)
                    {
                        Grid[row, col].LiveNeighbors = 9;

                        // Alert upper left cell
                        if (row - 1 >= 0 && col - 1 >= 0)
                        {
                            if (!Grid[row - 1, col - 1].Live)
                            {
                                Grid[row - 1, col - 1].LiveNeighbors++;
                            }
                        }

                        // Alert above cell
                        if (row - 1 >= 0)
                        {
                            if (!Grid[row - 1, col].Live)
                            {
                                Grid[row - 1, col].LiveNeighbors++;
                            }
                        }

                        // Alert upper right cell
                        if (row - 1 >= 0 && col + 1 <= Size - 1)
                        {
                            if (!Grid[row - 1, col + 1].Live)
                            {
                                Grid[row - 1, col + 1].LiveNeighbors++;
                            }
                        }

                        // Alert left cell
                        if (col - 1 >= 0)
                        {
                            if (!Grid[row, col - 1].Live)
                            {
                                Grid[row, col - 1].LiveNeighbors++;
                            }
                        }

                        // Alert right cell
                        if (col + 1 <= Size - 1)
                        {
                            if (!Grid[row, col + 1].Live)
                            {
                                Grid[row, col + 1].LiveNeighbors++;
                            }
                        }

                        // Alert lower left cell
                        if (row + 1 <= Size - 1 && col - 1 >= 0)
                        {
                            if (!Grid[row + 1, col - 1].Live)
                            {
                                Grid[row + 1, col - 1].LiveNeighbors++;
                            }
                        }

                        // Alert lower cell
                        if (row + 1 <= Size - 1)
                        {
                            if (!Grid[row + 1, col].Live)
                            {
                                Grid[row + 1, col].LiveNeighbors++;
                            }
                        }

                        // Alert lower right cell
                        if (row + 1 <= Size - 1 && col + 1 <= Size - 1)
                        {
                            if (!Grid[row + 1, col + 1].Live)
                            {
                                Grid[row + 1, col + 1].LiveNeighbors++;
                            }
                        }
                    }
                }
            }
        }

        public void floodFill(int row, int col)
        {
            Grid[row, col].Visited = true;
            moves++;
            if (gridCheck("col", Grid[row, col], 1)) floodFill(row, col + 1); // Flood fill right
            if (gridCheck("col", Grid[row, col], -1)) floodFill(row, col - 1); // Flood fill left
            if (gridCheck("row", Grid[row, col], 1)) floodFill(row + 1, col); // Flood fill down
            if (gridCheck("row", Grid[row, col], -1)) floodFill(row - 1, col); // Flood fill up

            // Diagonal fills
            if (gridCheck("diag", Grid[row, col], -1, -1)) floodFill(row - 1, col - 1); // Flood fill up left
            if (gridCheck("diag", Grid[row, col], -1, 1)) floodFill(row - 1, col + 1); // Flood fill up right
            if (gridCheck("diag", Grid[row, col], 1, -1)) floodFill(row + 1, col - 1); // Flood fill down left
            if (gridCheck("diag", Grid[row, col], 1, 1)) floodFill(row + 1, col + 1); // Flood fill down right
        }

        // This method will determine if 1. Cell is in bounds, 2. Cell is not visited,
        // 3. Cell is not live, 4. Cell has 0 live neighbors
        public bool gridCheck(string value, CellModel cell, int step, int step2 = 0) // step2 is optional parameter for diagonal check
        {
            switch (value)
            {
                case "row":
                    if (cell.Row + step >= 0 && cell.Row + step <= Size - 1) // check out of bounds
                    {
                        if (!Grid[cell.Row + step, cell.Column].Visited)
                        {
                            if (!Grid[cell.Row + step, cell.Column].Live) // Check if next cell is live
                            {
                                if (Grid[cell.Row + step, cell.Column].LiveNeighbors > 0 && Grid[cell.Row + step, cell.Column].LiveNeighbors <= 8) // Ensure liveNeighbors is 0
                                {
                                    Grid[cell.Row + step, cell.Column].Visited = true;
                                    moves++;
                                    return false;
                                }

                                if (Grid[cell.Row + step, cell.Column].LiveNeighbors == 0)
                                {
                                    Grid[cell.Row + step, cell.Column].Visited = true;
                                    return true;
                                }
                            }
                            return false;
                        }
                        return false;
                    }
                    return false;


                case "col":
                    if (cell.Column + step >= 0 && cell.Column + step <= Size - 1) // check out of bounds
                    {
                        if (!Grid[cell.Row, cell.Column + step].Visited)
                        {
                            if (!Grid[cell.Row, cell.Column + step].Live) // Check if next cell is live
                            {
                                if (Grid[cell.Row, cell.Column + step].LiveNeighbors > 0 && Grid[cell.Row, cell.Column + step].LiveNeighbors <= 8) // Ensure liveNeighbors is 0
                                {
                                    Grid[cell.Row, cell.Column + step].Visited = true;
                                    moves++;
                                    return false;
                                }
                                if (Grid[cell.Row, cell.Column + step].LiveNeighbors == 0)
                                {
                                    Grid[cell.Row, cell.Column + step].Visited = true;
                                    return true;
                                }
                            }
                            return false;
                        }
                        return false;
                    }
                    return false;

                case "diag":
                    if (cell.Row + step >= 0 && cell.Row + step <= Size - 1 && cell.Column + step2 >= 0 && cell.Column + step2 <= Size - 1) // check out of bounds
                    {
                        if (!Grid[cell.Row + step, cell.Column + step2].Visited)
                        {
                            if (!Grid[cell.Row + step, cell.Column + step2].Live) // Check if next cell is live
                            {
                                if (Grid[cell.Row + step, cell.Column + step2].LiveNeighbors > 0 && Grid[cell.Row + step, cell.Column + step2].LiveNeighbors <= 8) // Ensure liveNeighbors is 0
                                {
                                    Grid[cell.Row + step, cell.Column + step2].Visited = true;
                                    moves++;
                                    return false;
                                }
                                if (Grid[cell.Row + step, cell.Column + step2].LiveNeighbors == 0) // Ensure liveNeighbors is 0
                                {
                                    Grid[cell.Row + step, cell.Column + step2].Visited = true;
                                    return true;
                                }
                            }
                            return false;
                        }
                        return false;
                    }
                    return false;
            }
            return false;
        }
    }
}
