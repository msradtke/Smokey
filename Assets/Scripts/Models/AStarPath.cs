using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                var nextCell = Search(currentLocation, finish, gridArea);
                if (nextCell == null)
                    return gPath;
                var loc = gridArea.GetCellLocation(nextCell);
                if (loc.X < 0 || loc.Y < 0)
                    return gPath;
                if (Math.Abs(loc.X - currentLocation.X) + Math.Abs(loc.Y -currentLocation.Y)>1)
                    return gPath;
                gPath.Add(loc);
                nextCell.State = CellState.Path;
                currentLocation = loc;
            }

            return gPath;

        }
        Cell Search(GridAreaLocation start, GridAreaLocation finish, GridArea gridArea)
        {
            int bestScore = int.MaxValue;
            Cell bestCell = null;
            foreach (var neighbor in GetNeighbors(start, gridArea))
            {
                if (neighbor == null || neighbor.State != CellState.None)
                    continue;
                var score = GetManhattanDistance(gridArea.GetCellLocation(neighbor), finish);
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
