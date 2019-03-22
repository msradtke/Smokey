using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts.Models
{
    public class AStarPath
    {
        public List<GridAreaLocation> FindPath(GridAreaLocation start, GridAreaLocation finish, GridArea gridArea)
        {
            var gPath = new List<GridAreaLocation>();
            gPath.Add(start);
            var currentLocation = start;

            while(currentLocation != finish)
            {
				var nextCell = Search(currentLocation, finish, gridArea,gPath);
                if (nextCell == null)
                    return gPath;
                var loc = gridArea.GetCellLocation(nextCell);
				if (loc.X < 0 || loc.Y < 0) {
					Debug.Log ("A* invalid x, y");	
					return gPath;
						
				}
				if (Math.Abs (loc.X - currentLocation.X) + Math.Abs (loc.Y - currentLocation.Y) > 1) {
					Debug.Log ("A* invalid path");
					return gPath;

				}
				if (gPath.Contains (loc)) {
					Debug.Log ("A* duplicate path");
					return gPath;

				}
                gPath.Add(loc);
                nextCell.State = CellState.Path;
                currentLocation = loc;
            }

            return gPath;

        }
		Cell Search(GridAreaLocation start, GridAreaLocation finish, GridArea gridArea, List<GridAreaLocation> gPath)
        {
            int bestScore = int.MaxValue;
            Cell bestCell = null;
            foreach (var neighbor in GetNeighbors(start, gridArea))
            {
				if (neighbor == null || neighbor.State != CellState.None)
                    continue;
				var loc = gridArea.GetCellLocation (neighbor);
				if (gPath.Contains (loc))
					continue;
				
				var score = GetManhattanDistance(loc, finish);
                if (score < bestScore)
                {
                    bestScore = score;
                    bestCell = neighbor;
                }
            }
            return bestCell;
        }

        List<Cell> GetNeighbors(GridAreaLocation location, GridArea gridArea)
        {
            var top = gridArea.GetCell(location.Add(0, 1));
            var bottom = gridArea.GetCell(location.Add(0, -1));
            var right = gridArea.GetCell(location.Add(1, 0));
            var left = gridArea.GetCell(location.Add(-1, 0));

            return new List<Cell>
            {
                top,bottom,right,left
            };
        }

        int GetManhattanDistance(GridAreaLocation start, GridAreaLocation finish)
        {
            var x = Math.Abs(start.X - finish.X);
            var y = Math.Abs(start.Y - finish.Y);
            return x + y;
        }
    }
}
