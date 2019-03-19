using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public static class TestGrid
    {
        static readonly int _areaWidthMin = 5;
        static readonly int _areaWidthMax = 15;
        static readonly int _areaHeightMin = 5;
        static readonly int _areaHeightMax = 15;
        static System.Random ran;

        static TestGrid()
        {
            ran = new System.Random();
        }

        public static GridArea GetGridArea()
        {
            int width = ran.Next(_areaWidthMin, _areaWidthMax);
            int height = ran.Next(_areaHeightMin, _areaHeightMax);
            var gridArea = GridUtility.NewGridArea(width, height);
            //var location = GridUtility.GridLocation;

            return gridArea;
        }
    }
}
