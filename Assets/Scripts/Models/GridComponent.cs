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
			Cells = new List<Cell> ();
        }
		public List<Cell> Cells { get; set; }
		public Color Color {
			get;
			set;
		}
    }
}
