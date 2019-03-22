using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts.Models
{
    public class GridComponent
    {
        public GridComponent()
        {
            CellLocations = new List<CellLocation>();
        }
        public List<CellLocation> CellLocations{ get; set; }
        public void SetAllCellLocations()
        {
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                {
                    CellLocations.Add(new CellLocation(x, y));
                }
        }
        public Color Color
        {
            get;
            set;
        }
    }
}
