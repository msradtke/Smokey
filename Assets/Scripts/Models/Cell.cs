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

        }
        public GridTile Parent { get; set; }
        public CellLocation Location { get; set; }
		public CellState State {
			get;
			set;
		}

    }
    public enum CellState
    {
        None,
        Vertical,
        Horizontal,
        Component
    }
}
