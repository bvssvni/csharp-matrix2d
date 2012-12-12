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
    }
}

