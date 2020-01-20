using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Api_Classes
{
    [Serializable]
    public class User
    {
        public string email;
        public int lastMap;
        public int money;
        public Objeto[] objects;
        public string password;
        public string username;
        public int xPos;
        public int yPos;
        public int zPos;
    }
}
