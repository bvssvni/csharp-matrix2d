using System;

namespace Matrix2d
{
    /// <summary>
    /// A point consists of two values, X and Y.
    /// It is often referred to as a vector when describing the difference betwee two points.
    /// </summary>
    public struct Point
    {
        public double X, Y;
        
        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Calculates the length of the vector.
        /// </summary>
        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        /// <summary>
        /// Finds the angle of the vector in degrees.
        /// </summary>/
        public double Angle()
        {
            return Math.Atan2(Y, X) * 180.0 / Math.PI;
        }

        /// <summary>
        /// Find the angle of the vector in radians.
        /// </summary>
        /// <returns>
        /// Returns the angle of the vector in radians.
        /// </returns>
        public double AngleInRadians()
        {
            return Math.Atan2(Y, X);
        }

        /// <summary>
        /// Creates a vector pointing to the left.
        /// The vector created has the same length.
        /// </summary>
        public Point Left()
        {
            return new Point(-this.Y, this.X);
        }

        /// <summary>
        /// Creates a vector pointing to the right.
        /// The vector created has the same length.
        /// </summary>
        public Point Right()
        {
            return new Point(this.Y, -this.X);
        }

        /// <summary>
        /// Returns the difference vector from b to a.
        /// </summary>
        /// <param name='a'>
        /// The end point.
        /// </param>
        /// <param name='b'>
        /// The start point.
        /// </param>
        public static Point Difference(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        /// <summary>
        /// Creates a vector that has same direction but length 1.
        /// </summary>
        public Point Normalized()
        {
            var length = this.Length();
            return new Point(this.X / length, this.Y / length);
        }

        public static Point Add(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static double Dot(Point a, Point b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static double CrossRight(Point a, Point b)
        {
            return Point.Dot(a, b.Right());
        }

        public static double CrossLeft(Point a, Point b)
        {
            return Point.Dot(a, b.Left());
        }

        public static Point operator + (Point a, Point b)
        {
            return Point.Add(a, b);
        }

        public static Point operator - (Point a, Point b)
        {
            return Point.Difference(a, b);
        }

        public static double operator * (Point a, Point b)
        {
            return Point.Dot(a, b);
        }
    }
}

