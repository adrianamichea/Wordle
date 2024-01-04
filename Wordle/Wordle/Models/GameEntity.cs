using System;
using System.Linq;
using Wordle.Interfaces;

namespace Wordle.Models
{
    public class GameEntity: IGameEntity
    {
        private int _userID { get; set; }

        public int UserID
        {
            get { return _userID; }
            set
            {
                if (_userID != value)
                {
                    _userID = value;
                }
            }
        }
        private string _secretWord { get; set; }

        public string SecretWord
        {
            get { return _secretWord; }
            set
            {
                if (_secretWord != value)
                {
                    _secretWord = value;
                }
            }
        }

        private string[] _attempts { get; set; }

        public string[] Attempts
        {
            get { return _attempts; }
            set
            {
                if (_attempts != value)
                {
                    _attempts = value;
                }
            }
        }

        private string[] _codes { get; set; }

        public string[] Codes
        {
            get { return _codes; }
            set
            {
                if (_codes != value)
                {
                    _codes = value;
                }
            }
        }
        public GameEntity() { }

        public GameEntity(int userID, string secretWord,string[] attempts, string[] codes)
        {
            UserID = userID;
            SecretWord = secretWord;
            Attempts = attempts;
            Codes = codes;
        }
        public GameEntity(string initialWord)
        {
            SecretWord = initialWord;
            Attempts = new string[6];
            Codes = new string[6];
        }

    }
}
