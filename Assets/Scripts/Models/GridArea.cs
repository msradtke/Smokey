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
        public int TileCount { get { return Width * Height; } }
        public List<GridModel> Grids
        {
            get;
            private set;
        }
        public void SetGrids(List<GridModel> gridModels)
        {
            Grids = gridModels;
            SetCells();
        }
        void SetCells()
        {
            Cells = new List<Cell>();
            if (Grids.Count < 1)
                return;
            int yCount = 0;
            int gridRow = 0;
            for (int y = 0; y < Height * 3; y++)
            {

                int gridCol = gridRow * Width;
                int cellIndex = 3 * yCount;
                
                for (int x = 0; x < Width * 3; x++)
                {
                    if (gridCol > Grids.Count || cellIndex > 8)
                        return;
                    //Grids[gridIndex].Cells[gridCol].Location = new CellLocation(x, yIndex);
                    Cells.Add(Grids[gridCol].Cells[cellIndex]); //for 3  keep the same Grid  (gridCol same)

                    cellIndex++;
                    if (cellIndex % 3 == 0)
                    {
                        gridCol++;
                        cellIndex = 3 * yCount;
                    }
                    
                }
                yCount++;
                if (yCount % 3 == 0)
                {
                    yCount = 0;
                    gridRow++;
                    
                }
                
            }

        }
        public List<Cell> Cells { get; set; }

        public GridLocation GetGridLocation(GridModel grid)
        {
            var index = Grids.IndexOf(grid);
            int x = index % Width;
            int y = (index + Width - 1) / Width;
            return new GridLocation(x, y);
        }
        public Cell GetCell(GridAreaLocation location)
        {
            var index = location.Y * Width + location.X;
            return Cells[index];
        }
    }
}
