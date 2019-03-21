using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class GridPath
    {
        public CellLocation Start { get; set; }
        public CellLocation Finish { get; set; }
        public CellLocation StartEntrance { get; set; }
        public CellLocation FinishEntrance { get; set; }
        public List<GridAreaLocation> Path { get; set; }
    }
}
