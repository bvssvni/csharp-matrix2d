using System;

namespace Matrix2d
{
    public class Matrix
    {
        public double[] Elements = new double[6];
        
        public Matrix()
        {
            Elements[0] = 1;
            Elements[4] = 1;
        }
        
        public Matrix(double m11, double m12, double m13, double m21, double m22, double m23)
        {
            Elements[0] = m11;
            Elements[1] = m12;
            Elements[2] = m13;
            Elements[3] = m21;
            Elements[4] = m22;
            Elements[5] = m23;
        }
        
        public bool IsIdentity()
        {
            return Elements[0] == 1 &&
                Elements[1] == 0 &&
                    Elements[2] == 0 &&
                    Elements[3] == 0 &&
                    Elements[4] == 1 &&
                    Elements[5] == 0;
        }
        
        public static Matrix Translation(double x, double y)
        {
            var mat = new Matrix();
            mat.Elements[2] = x;
            mat.Elements[5] = y;
            return mat;
        }
        
        public static Matrix Rotation(double angle)
        {
            var mat = new Matrix();
            double cos = Math.Cos(angle * Math.PI / 180.0);
            double sin = Math.Sin(angle * Math.PI / 180.0);
            mat.Elements[0] = cos;
            mat.Elements[1] = -sin;
            mat.Elements[3] = sin;
            mat.Elements[4] = cos;
            return mat;
        }
        
        public static Matrix Scale(double s)
        {
            return new Matrix(s, 0, 0, 0, s, 0);
        }
        
        public static Matrix RotationByRadians(double angle)
        {
            var mat = new Matrix();
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            mat.Elements[0] = cos;
            mat.Elements[1] = -sin;
            mat.Elements[3] = sin;
            mat.Elements[4] = cos;
            return mat;
        }
        
        public static Matrix Multiply(Matrix a, Matrix b)
        {
            var mat = new Matrix();
            mat.Elements[0] = a.Elements[0] * b.Elements[0] + a.Elements[1] * b.Elements[3];
            mat.Elements[1] = a.Elements[0] * b.Elements[1] + a.Elements[1] * b.Elements[4];
            mat.Elements[2] = a.Elements[0] * b.Elements[2] + a.Elements[1] * b.Elements[5] + a.Elements[2];
            mat.Elements[3] = a.Elements[3] * b.Elements[0] + a.Elements[4] * b.Elements[3];
            mat.Elements[4] = a.Elements[3] * b.Elements[1] + a.Elements[4] * b.Elements[4];
            mat.Elements[5] = a.Elements[3] * b.Elements[2] + a.Elements[4] * b.Elements[5] + a.Elements[5];
            return mat;
        }
        
        public double Determinant()
        {
            return Elements[0] * Elements[4] - Elements[1] * Elements[3];
        }
        
        public bool IsInvertible()
        {
            return Determinant() != 0;
        }
        
        public Matrix Inverted()
        {
            Matrix mat = new Matrix();
            double det = this.Determinant();
            
            mat.Elements[0] = Elements[4] / det;
            mat.Elements[1] = -Elements[1] / det;
            mat.Elements[2] = (Elements[1] * Elements[5] - Elements[4] * Elements[2]) / det;
            mat.Elements[3] = -Elements[3] / det;
            mat.Elements[4] = Elements[0] / det;
            mat.Elements[5] = (Elements[2] * Elements[3] - Elements[0] * Elements[5]) / det;
            
            return mat;
        }
        
        public bool IsTranslating()
        {
            return Elements[0] == 1 && Elements[1] == 0 && Elements[3] == 0 && Elements[4] == 1;
        }
    }
}

