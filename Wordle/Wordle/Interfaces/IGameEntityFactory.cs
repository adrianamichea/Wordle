﻿using System;
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
    }
}
