using System;
using NUnit.Framework;

namespace Matrix2d
{
    [TestFixture()]
    public class TestPolygon
    {
        [Test()]
        public void TestLength1()
        {
            var p = new Point(3, 4);
            Assert.True(p.Length() == 5);
            
            var p2 = new Point(0, 1);
            Assert.True(p2.Angle() == 90);
            Assert.True(p2.AngleInRadians() == Math.PI/2);
            
            Assert.True(p2.Left().X == -1);
            Assert.True(p2.Right().X == 1);
            
            var p3 = Point.Difference(p, p2);
            Assert.True(p3.X == 3);
            Assert.True(p3.Y == 3);
        }

        [Test()]
        public void TestNormalized1()
        {
            var p = new Point(4, 4);
            var q = p.Normalized();
            double eps = 0.00000001;
            Assert.True(Math.Abs(q.Length() - 1) < eps);
        }
        
        [Test()]
        public void TestMatrix1()
        {
            var matrix = new Matrix();
            Assert.True(matrix.IsIdentity());
        }
        
        [Test()]
        public void TestMatrix2()
        {
            var matrix = Matrix.Translation(10, 5);
            Assert.True(matrix.Elements[2] == 10);
            Assert.True(matrix.Elements[5] == 5);
            
            var matrix2 = Matrix.Translation(5, 10);
            var matrix3 = Matrix.Multiply(matrix2, matrix);
            Assert.True(matrix3.Elements[2] == 15);
            Assert.True(matrix3.Elements[5] == 15);
            
            Assert.True(matrix3.IsInvertible());
            Assert.True(matrix3.IsTranslating());
        }
        
        [Test()]
        public void TestMatrixInverted1()
        {
            var matrix = new Matrix(1,2,3,0,1,4);
            var inverted = matrix.Inverted();
            
            Assert.True(inverted.Elements[0] == 1);
            Assert.True(inverted.Elements[1] == -2);
            Assert.True(inverted.Elements[2] == 5);
            Assert.True(inverted.Elements[3] == 0);
            Assert.True(inverted.Elements[4] == 1);
            Assert.True(inverted.Elements[5] == -4);
        }
        
        [Test()]
        public void TestMatrixRotation1()
        {
            var mat = Matrix.Rotation(45);
            var res = Matrix.Multiply(mat, mat);
            double eps = 0.0000001;
            Assert.True(Math.Abs(res.Elements[0] - 0) < eps);
            Assert.True(Math.Abs(res.Elements[1] - -1) < eps);
            Assert.True(res.Elements[2] == 0);
            Assert.True(Math.Abs(res.Elements[3] - 1) < eps);
            Assert.True(Math.Abs(res.Elements[4] - 0) < eps);
            Assert.True(res.Elements[5] == 0);
        }
        
        [Test()]
        public void TestMatrixScaled1()
        {
            var mat = Matrix.Scale(5);
            Assert.True(mat.Elements[0] == 5);
            Assert.True(mat.Elements[1] == 0);
            Assert.True(mat.Elements[2] == 0);
            Assert.True(mat.Elements[3] == 0);
            Assert.True(mat.Elements[4] == 5);
            Assert.True(mat.Elements[5] == 0);
        }

        [Test()]
        public void TestDot1()
        {
            var a = new Point(4, 5);
            var b = new Point(0, 1);
            var dot = a * b;
            Assert.True(dot == 5);
            var crossRight = Point.CrossRight(a, b);
            Assert.True(crossRight == 4);
            var crossLeft = Point.CrossLeft(a, b);
            Assert.True(crossLeft == -4);
        }

        [Test()]
        public void TestTransform1()
        {
            Point[] points = new Point[]{new Point(0, 0), new Point(1, 1)};
            var mat = Matrix.Translation(2, 2);
            Point.TransformPoints(points, points, mat);
            Assert.True(points[0].X == 2);
            Assert.True(points[0].Y == 2);
            Assert.True(points[1].X == 3);
            Assert.True(points[1].Y == 3);
        }

        [Test()]
        public void TestTransformVector1()
        {
            var a = new Point(1, 1);
            var mat = Matrix.Translation(1, 1) * Matrix.Scale(2);
            var b = a.TransformVector(mat);
            Assert.True(b.X == 2);
            Assert.True(b.Y == 2);
            var c = a.Transform(mat);
            Assert.True(c.X == 3);
            Assert.True(c.Y == 3);
        }

        [Test()]
        public void TestArea()
        {
            var points = new Point[]{new Point(0, 0), new Point(100, 0), new Point(100, 100), new Point(0, 100)};
            var area = Point.AreaRightSideOut(points);
            Assert.True(area == -10000);
            Assert.True(Point.AreaLeftSideOut(points) == 10000);
        }

        [Test()]
        public void TestAverage()
        {
            var points = new Point[]{new Point(0, 0), new Point(100, 0), new Point(100, 100), new Point(0, 100)};
            var avg = Point.Average(points);
            Assert.True(avg.X == 50);
            Assert.True(avg.Y == 50);
        }

        [Test()]
        public void TestAngleBetween()
        {
            var a = new Point(100, 0);
            var b = new Point(0, 100);
            var angle = Point.AngleBetween(b, a);
            Assert.True(angle == 90);
        }

        [Test()]
        public void TestMultiplyScalar1()
        {
            var a = new Point(1, 1);
            a *= 3;
            Assert.True(a.X == 3);
            Assert.True(a.Y == 3);
        }

		[Test()]
		public void TestMatrixRotation()
		{
			var mat = Matrix.Rotation(90);

            double eps = 0.0000001;
			Assert.True(Math.Abs(mat.Elements[0] - 0) < eps);
            Assert.True(Math.Abs(mat.Elements[1] - -1) < eps);
            Assert.True(Math.Abs(mat.Elements[2] - 0) < eps);
            Assert.True(Math.Abs(mat.Elements[3] - 1) < eps);
            Assert.True(Math.Abs(mat.Elements[4] - 0) < eps);
            Assert.True(Math.Abs(mat.Elements[5] - 0) < eps);

            var p = new Point(0, 0).Transform(mat);
            Assert.True(Math.Abs(p.X - 0) < eps);
            Assert.True(Math.Abs(p.Y - 0) < eps);

            var q = new Point(100, 10).Transform(mat);
            Console.WriteLine(q.X.ToString());
            Assert.True(Math.Abs(q.X - -10) < eps);
            Assert.True(Math.Abs(q.Y - 100) < eps);
		}

		[Test()]
		public void TestRectangleTransformedMaximum()
		{
			var rect = new Rectangle(0, 0, 100, 10);
			var mat = Matrix.Rotation(90);
			var transformedRect = rect.TransformedMaximum(mat);

            double eps = 0.00000001;
            Assert.True(Math.Abs(transformedRect.Left - -10) < eps);
            Assert.True(Math.Abs(transformedRect.Top - 0) < eps);
            Assert.True(Math.Abs(transformedRect.Right - 0) < eps);
            Assert.True(Math.Abs(transformedRect.Bottom - 100) < eps);
		}
		
        [Test()]
        public void TestRectangleArea()
        {
            var rect = new Rectangle(0, 0, 100, 10);
            var area = rect.Area();

            Assert.True(area == 1000);
        }

        [Test()]
        public void TestRectangleCeiled()
        {
            var rect = new Rectangle(0.5, 0.5, 1.5, 2.5).Ceiled();

            Assert.True(rect.Left == 0);
            Assert.True(rect.Top == 0);
            Assert.True(rect.Right == 2);
            Assert.True(rect.Bottom == 3);
        }

        [Test()]
        public void TestRectangleContains()
        {
            var rect = new Rectangle(0, 0, 100, 100);
            var inner = new Rectangle(10, 10, 50, 50);

            Assert.True(rect.Contains(inner));
        }

        [Test()]
        public void TestRectangleIntersects()
        {
            var rect = new Rectangle(0, 0, 100, 100);
            var rect2 = new Rectangle(10, 0, 110, 100);

            Assert.True(rect.Intersects(rect2));
            Assert.True(rect2.Intersects(rect));

            var rect3 = new Rectangle(100, 0, 200, 200);

            Assert.False(rect.Intersects(rect3));
            Assert.False(rect3.Intersects(rect));

            Assert.True(rect2.Intersects(rect3));
            Assert.True(rect3.Intersects(rect2));
        }

        [Test()]
        public void TestRectangleClip()
        {
            var rect = new Rectangle(0, 0, 100, 100);
            var view = new Rectangle(10, 10, 80, 80);
            var clipped = rect.Clip(view);

            Assert.True(clipped == view);

            view = new Rectangle(0, 0, 200, 200);
            clipped = rect.Clip(view);

            Assert.True(clipped == rect);

            var a = new Rectangle(0, 0, 1024, 768);
            var b = new Rectangle(200, 0, 1024 + 200, 768);
            clipped = b.Clip(a);

            Assert.True(clipped.Left == 200);
            Assert.True(clipped.Top == 0);
            Assert.True(clipped.Right == 1024);
            Assert.True(clipped.Bottom == 768);
        }

        [Test()]
        public void TestMatrixInverted2()
        {
            var a = new Rectangle(0, 0, 1024, 768);
            var mat = Matrix.Translation(100, 0);
            var b = a.TransformedMaximum(mat);

            Assert.True(b.Left == 100);
            Assert.True(b.Top == 0);
            Assert.True(b.Right == 1024 + 100);
            Assert.True(b.Bottom == 768);

            mat = mat.Inverted();
            var c = b.TransformedMaximum(mat);

            Assert.True(c == a);
        }
    }
}

