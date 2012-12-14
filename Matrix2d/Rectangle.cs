using System;

namespace Matrix2d
{
	/// <summary>
	/// A rectangle with left and right boundaries, top and bottom.
	/// </summary>
	public class Rectangle : IComparable
	{
		public double Left;
		public double Top;
		public double Right;
		public double Bottom;

		public Rectangle(double left, double top, double right, double bottom)
		{
			this.Left = left;
			this.Top = top;
			this.Right = right;
			this.Bottom = bottom;
		}

        /// <summary>
        /// Calculates the area of the rectangle.
        /// </summary>
        public double Area()
        {
            return (this.Right - this.Left) * (this.Bottom - this.Top);
        }

		/// <summary>
		/// Returns a polygon of the corners of this rectangle.
		/// </summary>
		public Point[] Corners()
		{
			return new Point[]{new Point(Left, Top), new Point(Right, Top), new Point(Right, Bottom), new Point(Left, Bottom)};
		}

        /// <summary>
        /// Finds the maximum transformed rectangle.
        /// </summary>
        /// <returns>
        /// Returns a rectangle containing all potential affected coordinates.
        /// </returns>
        /// <param name='mat'>
        /// The matrix transformation.
        /// </param>
		public Rectangle TransformedMaximum(Matrix mat)
		{
			var corners = Corners();
			Point.TransformPoints(corners, corners, mat);

			var rect = new Rectangle(double.MaxValue, double.MaxValue, double.MinValue, double.MinValue);
			foreach (var point in corners)
			{
				if (point.X < rect.Left) rect.Left = point.X;
                if (point.Y < rect.Top) rect.Top = point.Y;
				if (point.X > rect.Right) rect.Right = point.X;
				if (point.Y > rect.Bottom) rect.Bottom = point.Y;
			}
			return rect;
		}

        /// <summary>
        /// Returns a rectangle with nearest integers containing this rectangle.
        /// </summary>
        public Rectangle Ceiled()
        {
            return new Rectangle(Math.Floor(Left), Math.Floor(Top), Math.Ceiling(Right), Math.Ceiling(Bottom));
        }

        /// <summary>
        /// Finds out whether a rectangle contains another.
        /// </summary>
        /// <param name='rect'>
        /// The rectangle to check whether it is inside.
        /// </param>
        public bool Contains(Rectangle rect)
        {
            return Left <= rect.Left
                && Top <= rect.Right
                && Right >= rect.Right
                && Bottom >= rect.Bottom;
        }

        /// <summary>
        /// Finds out whether there is any intersection between two rectangles.
        /// If only the edges overlap, there is no intersection.
        /// </summary>
        /// <param name='rect'>
        /// The rectangle to check for intersection.
        /// </param>
        public bool Intersects(Rectangle rect)
        {
            return Left < rect.Right
                && Top < rect.Bottom
                && Right > rect.Left
                && Bottom > rect.Top;
        }

        /// <summary>
        /// Clips a rectangle to view.
        /// </summary>
        /// <param name='rect'>
        /// Rect.
        /// </param>
        public Rectangle Clip(Rectangle view)
        {
            return new Rectangle(
                Left < view.Left ? view.Left : Left,
                Top < view.Top ? view.Top : Top,
                Right > view.Right ? view.Right : Right,
                Bottom > view.Bottom ? view.Bottom : Bottom
                );
        }

        public int CompareTo(object obj)
        {
            Rectangle b = (Rectangle)obj;
            if (Left < b.Left) return -1;
            if (Left > b.Left) return 1;
            if (Top < b.Top) return -1;
            if (Top > b.Top) return 1;
            if (Right < b.Right) return -1;
            if (Right > b.Right) return 1;
            if (Bottom < b.Bottom) return -1;
            if (Bottom > b.Bottom) return 1;
            return 0;
        }

        public static bool operator == (Rectangle a, Rectangle b)
        {
            return a.CompareTo(b) == 0;
        }

        public static bool operator != (Rectangle a, Rectangle b)
        {
            return a.CompareTo(b) != 0;
        }

        public static bool operator < (Rectangle a, Rectangle b)
        {
            return a.CompareTo(b) < 0;
        }

        public static bool operator <= (Rectangle a, Rectangle b)
        {
            return a.CompareTo(b) <= 0;
        }

        public static bool operator > (Rectangle a, Rectangle b)
        {
            return a.CompareTo(b) > 0;
        }

        public static bool operator >= (Rectangle a, Rectangle b)
        {
            return a.CompareTo(b) >= 0;
        }
	}
}

