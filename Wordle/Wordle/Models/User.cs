

using Wordle.Interfaces;

namespace Wordle.Models
{
    public class User: IUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        }
}
