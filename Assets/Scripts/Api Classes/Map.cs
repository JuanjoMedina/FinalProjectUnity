using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Api_Classes
{
    [Serializable]
    public class Map
    {
        public int name;
        public string data;

        public Map(int n, string a)
        {
            this.data = a;
            this.name = n;
        }
    }
}
