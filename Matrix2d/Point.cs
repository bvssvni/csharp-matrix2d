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

        public Point Transform(Matrix mat)
        {
            return new Point(mat.Elements[0] * X + mat.Elements[1] * Y + mat.Elements[2],
                             mat.Elements[3] * X + mat.Elements[4] * Y + mat.Elements[5]);
        }

        public Point TransformVector(Matrix mat)
        {
            return new Point(mat.Elements[0] * X + mat.Elements[1] * Y,
                             mat.Elements[3] * X + mat.Elements[4] * Y);
        }

        /// <summary>
        /// Transforms vectors, given a destination array and a source array.
        /// This transformation uses the translation part of the matrix.
        /// </summary>
        /// <param name='dest'>
        /// The destination array to transform vectors.
        /// </param>
        /// <param name='src'>
        /// The source array to transform vectors to.
        /// </param>
        /// <param name='mat'>
        /// The matrix transformation to apply.
        /// </param>
        public static void TransformPoints(Point[] dest, Point[] src, Matrix mat)
        {
            int n = src.Length;
            for (int i = 0; i < n; i++)
            {
                dest[i] = src[i].Transform(mat);
            }
        }

        /// <summary>
        /// Transforms vectors, given a destination array and a source array.
        /// This transformation does not use the translation part of the matrix.
        /// </summary>
        /// <param name='dest'>
        /// The destination array to transform vectors.
        /// </param>
        /// <param name='src'>
        /// The source array to transform vectors to.
        /// </param>
        /// <param name='mat'>
        /// The matrix transformation to apply.
        /// </param>
        public static void TransformVectors(Point[] dest, Point[] src, Matrix mat)
        {
            int n = src.Length;
            for (int i = 0; i < n; i++)
            {
                dest[i] = src[i].TransformVector(mat);
            }
        }

        /// <summary>
        /// Translates an array of points from a source to a destination.
        /// </summary>
        /// <param name='dest'>
        /// The destination array.
        /// </param>
        /// <param name='src'>
        /// The source array.
        /// </param>
        public void Translate(Point[] dest, Point[] src)
        {
            int n = src.Length;
            for (int i = 0; i < n; i++)
            {
                dest[i] = src[i] + this;
            }
        }

        /// <summary>
        /// Finds the index of the closest point in an array.
        /// </summary>
        /// <param name='src'>
        /// An array of points to search for the closest.
        /// </param>
        public int Closest(Point[] src)
        {
            int n = src.Length;
            double min = double.MaxValue;
            int minIndex = -1;
            for (int i = 0; i < n; i++)
            {
                double dx = src[i].X - this.X;
                double dy = src[i].Y - this.Y;
                double dist2 = dx * dx + dy * dy;
                if (dist2 < min) 
                {
                    min = dist2;
                    minIndex = i;
                }
            }
            return minIndex;
        }

        /// <summary>
        /// Calculates the area of an irregular polygon where right side is out.
        /// If the polygon is inverted a negative array will be returned.
        /// </summary>
        /// <returns>
        /// Returns area of irregular polygon with right side out.
        /// </returns>
        /// <param name='src'>
        /// An array containing the coordinates of the irregular polygon.
        /// </param>
        public static double AreaRightSideOut(Point[] src)
        {
            int n = src.Length;
            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                int j = (i+1)%n;
                Point a = src[i];
                Point b = src[j];
                sum += (b.Y + a.Y) * (b.X - a.X) / 2;
            }
            return sum;
        }

        /// <summary>
        /// Calculates the area of an irregular polygon where left side is out.
        /// If the polygon is inverted a negative array will be returned.
        /// </summary>
        /// <returns>
        /// Returns area of irregular polygon with left side out.
        /// </returns>
        /// <param name='src'>
        /// An array containing the coordinates of the irregular polygon.
        /// </param>
        public static double AreaLeftSideOut(Point[] src)
        {
            return -AreaRightSideOut(src);
        }
    }
}

