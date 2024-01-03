using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Interfaces
{
    public interface IGameEntity
    {
        int UserID { get; set; }
        string SecretWord { get; set; }
        DateTime Date { get; set; }
        string[] Attempts { get; set; }
        string[] Codes { get; set; }
    }
}
