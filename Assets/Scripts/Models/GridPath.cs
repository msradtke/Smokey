using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class GridPath
    {
        public GridAreaLocation Start { get; set; }
        public GridAreaLocation Finish { get; set; }
        public GridAreaLocation StartEntrance { get; set; }
        public GridAreaLocation FinishEntrance { get; set; }
        public List<GridAreaLocation> Path { get; set; }
    }
}
