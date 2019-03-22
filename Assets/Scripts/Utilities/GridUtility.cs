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
        static float outerCellWidth = 34;

        static GridUtility()
        {
            GridLocation = new Vector3(0, 10000, 0);
        }

        public static GridArea NewGridArea(int width, int height)
        {
            var gridArea = new GridArea(width, height, GridLocation);

            return gridArea;
        }
        public static Vector3 GetCellPosition(GridComponent component)
        {

            if (component.GetType() == typeof(GridComponent))
            {
                var cell = component.CellLocations.First();

                //start is (1,1) aka middle
                var xChange = cell.X - 1;
                var yChange = cell.Y - 1;
                var xThicknessAdded = gridLineThickness * xChange;
                var yThicknessAdded = gridLineThickness * yChange;
                //var centerDistance =  / cellWidth;

                var x = xChange * cellWidth + xThicknessAdded;
                var y = yChange * cellWidth + yThicknessAdded;

                var divX = x == 0 ? 1 : ppu;
                var divY = y == 0 ? 1 : ppu;
                return new Vector3(x / divX, y / divY, 0);
            }
            if (component.GetType() == typeof(Chip))
            {
                return Vector3.zero;
            }
            return Vector3.zero;
        }

        public static Vector3 GetSpritePositionForPath(CellLocation location)
        {
            var xChange = location.X - 1; // middle is 1,1
            var yChange = location.Y - 1;

            //var centerDistance =  / cellWidth;
            var xThicknessAdded = gridLineThickness * xChange;
            var yThicknessAdded = gridLineThickness * yChange;

            var x = xChange * outerCellWidth - xThicknessAdded;
            var y = yChange * outerCellWidth - yThicknessAdded;
            var divX = x == 0 ? 1 : ppu;
            var divY = y == 0 ? 1 : ppu;
            float xRet = x / divX;
            float yRet = y / divY;
            return new Vector3(xRet, yRet, 0);
        }

        public static Vector3 GetSpritePositionForPathCrossing(Cell subject, Cell other)
        {
            float change = 16; //need move it up or right etc 16picleas
            int xChange = subject.Location.X - 1; // middle is 1,1
            int yChange = subject.Location.Y - 1;

            float xPixelAdd = 0;
            float yPixelAdd = 0;
            if (subject.TopNeighbor == other)
                yPixelAdd = change;
            if (subject.BottomNeighbor == other)
                yPixelAdd = -change;
            if (subject.RightNeighbor == other)
                xPixelAdd = change;
            if (subject.LeftNeighbor == other)
                xPixelAdd = -change;


            //var centerDistance =  / cellWidth;
            var xThicknessAdded = gridLineThickness * xChange;
            var yThicknessAdded = gridLineThickness * yChange;

            var x = xChange * outerCellWidth - xThicknessAdded + xPixelAdd;
            var y = yChange * outerCellWidth - yThicknessAdded + yPixelAdd;
            var divX = x == 0 ? 1 : ppu;
            var divY = y == 0 ? 1 : ppu;
            float xRet = x / divX;
            float yRet = y / divY;
            return new Vector3(xRet, yRet, 0);
        }

        public static GridModel GetEmptyGrid(GridArea gridArea)
        {
            var ranList = new List<int>();
            for (int i = 0; i < gridArea.TileCount; i++)
            {
                ranList.Add(i);
            }
            ranList.Shuffle();

            foreach (var i in ranList)
            {
                var t = gridArea.Grids[i];
                if (t.IsEmpty())
                    return t;
            }

            return null;
        }
        public static Vector3 GridLocation { get; private set; }
        public static void GeneratePath(GridPath path, GridArea gridArea)
        {
            var start = path.Start;
            var end = path.Finish;
            var pathResult = new List<GridAreaLocation>();
            pathResult.Add(path.StartEntrance);

            bool isComplete = false;
            GridAreaLocation previous = path.StartEntrance;
            GridAreaLocation currentLocation = path.StartEntrance;


            while (!isComplete)
            {
                var cell = gridArea.GetCell(previous);
                //                for (int i = 0; i < 4; i++)
                //                {
                //                    var n = cell.Neighbors[i];
                //
                //				}
                var preferredNext = PathAlgorithm.SemiGeneticGetNextLocation(gridArea, currentLocation, path.FinishEntrance, previous);
                bool added = false;
                foreach (var i in preferredNext)
                {
                    var c = gridArea.GetCell(i.Add(currentLocation));
                    if (c == null)
                        continue;
                    if (c.State == CellState.None)
                    {

                        previous = currentLocation;
                        currentLocation = i.Add(currentLocation);
                        if (currentLocation.X < 0 || currentLocation.Y < 0)
                            return;
                        c.State = CellState.Path;
                        pathResult.Add(currentLocation);
                        added = true;

                        break;
                    }

                }
                if (!added)
                {
                    path.Path = pathResult;
                    return;
                }
                if (pathResult.Last() == path.FinishEntrance)
                {
                    path.Path = pathResult;
                    return;
                }
            }

            pathResult.Add(path.FinishEntrance);
        }
        static bool ValidatePath(GridPath gridPath)
        {
            GridAreaLocation previous = null;
            foreach (var l in gridPath.Path)
            {
                if (previous == null)
                {
                    previous = l;
                    continue;
                }
                if (Math.Abs(l.X - previous.X) + Math.Abs(l.Y - previous.Y) > 1)
                    return false; //error
                previous = l;
            }
            return true;
        }
        public static void SetCellStates(List<GridPath> gridPaths, GridArea gridArea)
        {
            foreach (var path in gridPaths)
            {
                ValidatePath(path);
                GridAreaLocation previousLocation = null;

                var p = path.Path;
                int previousIndex = -1;
                for (int i = 0; i < p.Count; i++)
                {

                    var currentLocation = p[i];
                    var cell = gridArea.GetCell(p[i]);
                    if (cell == null)
                        Console.WriteLine();
                    if (previousIndex >= 0)
                    {
                        var previous = gridArea.GetCell(p[i - 1]);
                        if (previousLocation.X != currentLocation.X)
                        {
                            if (previousLocation.X < currentLocation.X)
                            {// previuos is left
                                cell.State = CellState.Path;
                                cell.LeftNeighbor = previous;
                            }
                            else
                            {//previous is right
                                cell.State = CellState.Path;
                                cell.RightNeighbor = previous;
                            }
                        }
                        else
                        {
                            if (previousLocation.Y < currentLocation.Y)
                            {// previous is bottom
                                cell.State = CellState.Path;
                                cell.BottomNeighbor = previous;
                            }
                            else
                            {//previous is top
                                cell.State = CellState.Path;
                                cell.TopNeighbor = previous;
                            }
                        }
                    }
                    if (i == p.Count - 1)
                        continue;
                    previousLocation = currentLocation;

                    var nextLocation = p[i + 1];
                    var next = gridArea.GetCell(nextLocation);
                    if (nextLocation.X != currentLocation.X)
                    {
                        if (nextLocation.X < currentLocation.X)// next is left
                        {
                            cell.State = CellState.Path;
                            cell.LeftNeighbor = next;
                        }
                        else//next is right
                        {
                            cell.State = CellState.Path;
                            cell.RightNeighbor = next;
                        }
                    }
                    else
                    {
                        if (nextLocation.Y < currentLocation.Y)// next is bottom
                        {
                            cell.State = CellState.Path;
                            cell.BottomNeighbor = next;
                        }
                        else//next is top
                        {
                            cell.State = CellState.Path;
                            cell.TopNeighbor = next;
                        }
                    }


                    previousLocation = currentLocation;
                    previousIndex++;
                }
            }


        }
    }
}

