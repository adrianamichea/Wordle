using System;
using System.Linq;

namespace Wordle.Models
{
    public class GameEntity
    {
        public int UserID { get; set; }
        public String SecretWord { get; set; }
        public DateTime Date { get; set; }
        public string Attempts { get; set; }

        public GameEntity() { }

    }
}
