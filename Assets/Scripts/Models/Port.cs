using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class Port
    {
        public Port(int location)
        {
            Location = location;
        }

        public int Location { get; private set; }

    }
}
