using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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

		public GridAreaLocation Add(GridAreaLocation other)
		{
			return new GridAreaLocation (X + other.X, Y + other.Y);
		}
        public GridAreaLocation Add(int x, int y)
        {
            return new GridAreaLocation(X + x, Y + y);
        }
        public Vector2 GetRelativeDirection(GridAreaLocation other)
        {
            int x = 0;
            int y = 0;
            if (X > other.X)
                x = -1;
            if (X < other.X)
                x = 1;
            if (Y > other.Y)
                y = -1;
            if (Y < other.Y)
                y = 1;

            return new Vector2(x, y);
        }
        public bool Equals(GridAreaLocation other)
        {
			if(object.ReferenceEquals(other,null))
				return false;
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
			
            if (object.ReferenceEquals(lhs, null))
            {
                if (object.ReferenceEquals(rhs, null))
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
