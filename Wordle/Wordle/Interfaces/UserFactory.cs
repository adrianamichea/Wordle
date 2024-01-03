using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordle.Models;

namespace Wordle.Interfaces
{
    public class UserFactory: IUserFactory
    {
        public IUser CreateUser()
        {
            return new User();
        }
    }
}
