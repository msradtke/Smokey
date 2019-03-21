using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class GridLocation : IEquatable<GridLocation>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public GridLocation(int x , int y)
        {
            X = x;
            Y = y;
        }
        public bool Equals(GridLocation other)
        {
            if (X == other.X && Y == other.Y)
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as GridLocation);
        }
        public override int GetHashCode()
        {
            return X * 0x00010000 + Y;
        }
        public static bool operator ==(GridLocation lhs, GridLocation rhs)
        {
            // Check for null on left side.
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    // null == null = true.
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(GridLocation lhs, GridLocation rhs)
        {
            return !(lhs == rhs);
        }
    }
}
