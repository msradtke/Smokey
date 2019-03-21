using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts.Models
{
    public class GridModel
    {

        public GridModel(GridTile gridTile)
        {
            InitCells();
            GridComponents = new List<GridComponent>();
            this.GridTile = gridTile;
        }
		public List<Cell> Cells {
			get;
			set;
		}
		public GridTile GridTile {
			get;
			set;
		}
        public List<GridComponent> GridComponents { get; set; }

        public void AddComponent(GridComponent gridComponent)
        {
            GridComponents.Add(gridComponent);
            foreach(var cl in gridComponent.CellLocations)
            {
                var loc = Cells.FirstOrDefault(x => x.Location.X == cl.X && x.Location.Y == cl.Y);
                loc.State = CellState.Component;
            }
        }

        public bool IsEmpty()
		{
			foreach (var cell in Cells)
				if (cell.State != CellState.None)
					return false;
			return true;
		}

        void InitCells()
        {
            Cells = new List<Cell>();
			for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
                
                {
                Cells.Add(new Cell { Location = new CellLocation (x, y ), State = CellState.None });
            }
        }

    }
}
