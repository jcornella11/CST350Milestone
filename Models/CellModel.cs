namespace CST350Milestone.Models
{
    public class CellModel
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool Visited { get; set; }
        public bool Live { get; set; }
        public bool flagged { get; set; }
        public int LiveNeighbors { get; set; }

        public CellModel(int row, int column, bool visited, bool live, int liveNeighbors)
        {
            Row = row;
            Column = column;
            Visited = visited;
            Live = live;
            LiveNeighbors = liveNeighbors;
        }

        public CellModel()
        {
            Row = -1;
            Column = -1;
            Visited = false;
            Live = false;
            LiveNeighbors = 0;
        }
    }
}
