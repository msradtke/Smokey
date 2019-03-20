using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts.Models
{
    public class GridModel
    {

        public GridModel()
        {
			Cells = new List<Cell> ();
        }
		public List<Cell> Cells {
			get;
			set;
		}
		public GridTile GridTile {
			get;
			set;
		}

		public bool IsEmpty()
		{
			foreach (var cell in Cells)
				if (cell.State != CellState.None)
					return false;
			return true;
		}

    }
}
