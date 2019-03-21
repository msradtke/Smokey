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
        static int gridLineThickness = 3;
        static int cellWidth = 28;
        static int ppu = 128;
        static int outerCellWidth = 34;

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
            
			if (component.GetType() == typeof(GridComponent)) {
				var cell = component.CellLocations.First();

                //start is (1,1) aka middle
                var xChange = cell.X - 1;
				var yChange = cell.Y - 1;
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

        public static Vector3 GetCellPositionForPath(CellLocation location)
        {
            var xChange = location.X - 1; // middle is 1,1
            var yChange = location.Y - 1;

            //var centerDistance =  / cellWidth;

            var x = xChange * outerCellWidth;
            var y = yChange * outerCellWidth;

            return new Vector3(x / ppu, y / ppu, 0);
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

        public static void SetCellStates(List<GridPath> gridPaths, GridArea gridArea)
        {
            foreach(var path in gridPaths)
            {
                Cell previous = null;
                GridAreaLocation previousLocation = null;
                foreach (var loc in path.Path)
                {
                    var cell = gridArea.GetCell(loc);
                    if (previous != null)
                    {
                        if (previous.Location.X != cell.Location.X)
                            if (previous.Location.X < cell.Location.X)// previuos is left
                            {
                                cell.State = CellState.Path;
                                cell.LeftNeighbor = previous;
                            }
                            else//previous is right
                            {
                                cell.State = CellState.Path;
                                cell.RightNeighbor = previous;
                            }
                        else
                        {
                            if (previous.Location.Y < cell.Location.Y)// previous is bottom
                            {
                                cell.State = CellState.Path;
                                cell.BottomNeighbor = previous;
                            }
                            else//previous is top
                            {
                                cell.State = CellState.Path;
                                cell.TopNeighbor = previous;
                            }
                        }

                        cell = gridArea.GetCell(previousLocation);
                        if (previous != null)
                        {
                            if (previous.Location.X != cell.Location.X)
                                if (previous.Location.X < cell.Location.X)// previuos is left
                                {
                                    cell.State = CellState.Path;
                                    cell.LeftNeighbor = previous;
                                }
                                else//previous is right
                                {
                                    cell.State = CellState.Path;
                                    cell.RightNeighbor = previous;
                                }
                            else
                            {
                                if (previous.Location.Y < cell.Location.Y)// previous is bottom
                                {
                                    cell.State = CellState.Path;
                                    cell.BottomNeighbor = previous;
                                }
                                else//previous is top
                                {
                                    cell.State = CellState.Path;
                                    cell.TopNeighbor = previous;
                                }
                            }
                        }
                    }

                    previousLocation = loc;
                    previous = cell;
                }
            }
        }

    }
}
