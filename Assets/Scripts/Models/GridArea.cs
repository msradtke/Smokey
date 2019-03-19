using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class GridArea
    {
        public GridArea(int width, int height, Vector3 location)
        {
            Width = width;
            Height = height;
            Location = location;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public Vector3 Location { get; set; }


    }
}
