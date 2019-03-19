using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public static class GridUtility
    {

        static GridUtility()
        {
            GridLocation = new Vector3(0, 10000,0);
        }

        public static GridArea NewGridArea(int width,int height)
        {
            var gridArea = new GridArea(width,height,GridLocation);

            return gridArea;
        }

        public static Vector3 GridLocation { get; private set; }
    }
}
