using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class Cell
    {
        public Cell()
        {
            Neighbors = new Cell[4];
        }
        public GridTile Parent { get; set; }
        public CellLocation Location { get; set; }
		public CellState State {
			get;
			set;
		}

        public Cell[] Neighbors { get; set; }

        public Cell LeftNeighbor { get; set; }
        public Cell RightNeighbor { get; set; }
        public Cell TopNeighbor { get; set; }
        public Cell BottomNeighbor { get; set; }

    }
   
    public enum CellState
    {
        None,
        Path,
        Component
    }
}
