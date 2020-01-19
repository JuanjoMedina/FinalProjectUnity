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
        string name;
        string data;

        public string Name { get => name; set => name = value; }
        public string Data { get => data; set => data = value; }
    }
}
