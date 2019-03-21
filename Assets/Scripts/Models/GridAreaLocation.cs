using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class GridAreaLocation : IEquatable<GridAreaLocation>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public GridAreaLocation(int x, int y)
        {
            X = x;
            Y = y;
        }
        public bool Equals(GridAreaLocation other)
        {
            if (X == other.X && Y == other.Y)
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as GridAreaLocation);
        }
        public override int GetHashCode()
        {
            return X * 0x00010000 + Y;
        }
        public static bool operator ==(GridAreaLocation lhs, GridAreaLocation rhs)
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

        public static bool operator !=(GridAreaLocation lhs, GridAreaLocation rhs)
        {
            return !(lhs == rhs);
        }
    }
}
