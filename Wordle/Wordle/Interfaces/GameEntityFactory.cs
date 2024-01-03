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
    }
}
