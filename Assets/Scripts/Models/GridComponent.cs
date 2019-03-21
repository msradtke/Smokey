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

        public Color Color
        {
            get;
            set;
        }
    }
}
