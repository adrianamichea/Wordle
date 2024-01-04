using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Interfaces
{
    public interface IGameEntityFactory
    {
        IGameEntity CreateGameEntity();

        IGameEntity CreateGameEntity(string initialWord);

        IGameEntity ResumeGameEntity(int userID, string secretWord, string[] attempts, string[] codes);
    }
}
