namespace CST350Milestone.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        
        public DateTime gameTime { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string gameData { get; set; }

        public GameModel(int id, DateTime gameTime, string firstName, string lastName, string gameData)
        {
            Id = id;
            this.gameTime = gameTime;
            this.firstName = firstName;
            this.lastName = lastName;
            this.gameData = gameData;
        }

        public GameModel() { }
    }
}
