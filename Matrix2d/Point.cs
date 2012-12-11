using System;

namespace Matrix2d
{
    public struct Point
    {
        public double X, Y;
        
        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
        
        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }
        
        public double Angle()
        {
            return Math.Atan2(Y, X) * 180.0 / Math.PI;
        }
        
        public double AngleInRadians()
        {
            return Math.Atan2(Y, X);
        }
        
        public Point Left()
        {
            return new Point(-this.Y, this.X);
        }
        
        public Point Right()
        {
            return new Point(this.Y, -this.X);
        }
        
        // Returns the difference between two points.
        public static Point Difference(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }
    }
}

