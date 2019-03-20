using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Utilities;
namespace Assets.Scripts.Utilities
{
    public static class GridUtility
    {
        static float gridLineThickness = 3;
        static float cellWidth = 28;
        static float ppu = 128;
        static GridUtility()
        {
            GridLocation = new Vector3(0, 10000, 0);
        }

        public static GridArea NewGridArea(int width,int height)
        {
            var gridArea = new GridArea(width,height,GridLocation);

            return gridArea;
        }
		public static Vector3 GetCellPosition(GridComponent component)
        {
            //start is (1,1) aka middle
			if (component.GetType() == typeof(GridComponent)) {
				var cell = component.Cells.First();
				var xChange = cell.Location.X - 1;
				var yChange = cell.Location.Y - 1;
				var xThicknessAdded = gridLineThickness * xChange;
				var yThicknessAdded = gridLineThickness * yChange;
				//var centerDistance =  / cellWidth;

				var x = xChange * cellWidth + xThicknessAdded;
				var y = yChange * cellWidth + yThicknessAdded;

				return new Vector3 (x / ppu, y / ppu, 0);
			}
			if (component.GetType() == typeof(Chip)) 
			{
				return Vector3.zero;
			}
			return Vector3.zero;
        }

		public static GridModel GetEmptyGrid(GridArea gridArea)
		{
			var ranList = new List<int> ();
			for (int i = 0; i < gridArea.TileCount; i++) {
				ranList.Add (i);
			}
			ranList.Shuffle ();

			foreach(var i in ranList) {
				var t = gridArea.Grids [i];
				if (t.IsEmpty())
					return t;
			}

			return null;
		}
        public static Vector3 GridLocation { get; private set; }
    }
}
