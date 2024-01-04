using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordle.Models;

namespace Wordle.Interfaces
{
    public class GameEntityFactory: IGameEntityFactory
    {
        public IGameEntity CreateGameEntity()
        {
            return new GameEntity();
        }

        public IGameEntity CreateGameEntity(string initialWord)
        {
            return new GameEntity(initialWord.ToUpper());
        }

        public IGameEntity ResumeGameEntity(int userID, string secretWord, string[] attempts, string[] codes)
        {
            return new GameEntity(userID, secretWord, attempts, codes); 
        }
    }
}
