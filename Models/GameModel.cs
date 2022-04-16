using System.ComponentModel;

namespace CST350Milestone.Models
{
    public class GameModel
    {
        [DisplayName("Id Number")]
        public int Id { get; set; }
        
        [DisplayName("Game Time")]
        public DateTime gameTime { get; set; }

        [DisplayName("Player First Name")]
        public string firstName { get; set; }

        [DisplayName("Player Last Name")]
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
