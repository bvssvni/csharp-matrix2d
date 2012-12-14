using System;

namespace Matrix2d
{
    /// <summary>
    /// This is a matrix limited to 2D coordinates.
    /// A matrix represents a coordinate system.
    /// Different kinds of transformations can be combined with multiplication.
    /// It has only 6 entries because the last row with 0 0 1 is constant.
    /// </summary>
    public class Matrix
    {
        public double[] Elements = new double[6];

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix2d.Matrix"/> class.
        /// </summary>
        public Matrix()
        {
            Elements[0] = 1;
            Elements[4] = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix2d.Matrix"/> class.
        /// </summary>
        /// <param name='m11'>
        /// The element in 1st row and 1st column.
        /// </param>
        /// <param name='m12'>
        /// The element in 1st row and 2nd column.
        /// </param>
        /// <param name='m13'>
        /// The element in 1st row and 3rd column.
        /// This element contains the translation in the x-direction.
        /// </param>
        /// <param name='m21'>
        /// The element in 2nd row and 1st column.
        /// </param>
        /// <param name='m22'>
        /// The element in 2nd row and 2nd column.
        /// </param>
        /// <param name='m23'>
        /// The element in 2nd row nad 3rd column.
        /// This element contains the translation in the y-direction.
        /// </param>
        public Matrix(double m11, double m12, double m13, double m21, double m22, double m23)
        {
            Elements[0] = m11;
            Elements[1] = m12;
            Elements[2] = m13;
            Elements[3] = m21;
            Elements[4] = m22;
            Elements[5] = m23;
        }

        /// <summary>
        /// Determines whether this matrix is identity.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this matrix is identity; otherwise, <c>false</c>.
        /// </returns>
        public bool IsIdentity()
        {
            return Elements[0] == 1 &&
                Elements[1] == 0 &&
                    Elements[2] == 0 &&
                    Elements[3] == 0 &&
                    Elements[4] == 1 &&
                    Elements[5] == 0;
        }

        // Creates a translation matrix.
        /// <summary>
        /// Creates a translation matrix.
        /// </summary>
        /// <param name='x'>
        /// How much to translate in the x direction.
        /// </param>
        /// <param name='y'>
        /// How much to translate in the y direction.
        /// </param>
        public static Matrix Translation(double x, double y)
        {
            var mat = new Matrix();
            mat.Elements[2] = x;
            mat.Elements[5] = y;
            return mat;
        }

        // Creates a rotation matrix.
        /// <summary>
        /// Creates a rotation matrix in degrees specified by angle.
        /// </summary>
        /// <param name='angle'>
        /// The angle of rotation in degrees.
        /// </param>
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

        // Creates a scale matrix with one factor.
        /// <summary>
        /// Creates a scaling matrix with equal stretching in x and y direction.
        /// For direction dependent scaling, use "Stretch".
        /// </summary>
        /// <param name='s'>
        /// The scaling factor.
        /// </param>
        public static Matrix Scale(double s)
        {
            return new Matrix(s, 0, 0, 0, s, 0);
        }

        /// <summary>
        /// Stretch the specified matrix by sx and sy.
        /// </summary>
        /// <param name='sx'>
        /// Stretching in the x direction.
        /// </param>
        /// <param name='sy'>
        /// Stretching in the y direction.
        /// </param>
        public static Matrix Stretch(double sx, double sy)
        {
            return new Matrix(sx, 0, 0, 0, sy, 0);
        }

        /// <summary>
        /// Creates a rotation matrix by radians.
        /// </summary>
        /// <returns>
        /// Returns a rotation matrix.
        /// </returns>
        /// <param name='angle'>
        /// The angle of rotation in radians.
        /// </param>
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

        /// <summary>
        /// Composes two matrices together to form a new matrix.
        /// This equals combining two coordinate systems into a new one.
        /// </summary>
        /// <param name='a'>
        /// The child matrix.
        /// </param>
        /// <param name='b'>
        /// The parent matrix.
        /// </param>
        public static Matrix Multiply(Matrix child, Matrix parent)
        {
            var mat = new Matrix();
            mat.Elements[0] = child.Elements[0] * parent.Elements[0] + child.Elements[1] * parent.Elements[3];
            mat.Elements[1] = child.Elements[0] * parent.Elements[1] + child.Elements[1] * parent.Elements[4];
            mat.Elements[2] = child.Elements[0] * parent.Elements[2] + child.Elements[1] * parent.Elements[5] + child.Elements[2];
            mat.Elements[3] = child.Elements[3] * parent.Elements[0] + child.Elements[4] * parent.Elements[3];
            mat.Elements[4] = child.Elements[3] * parent.Elements[1] + child.Elements[4] * parent.Elements[4];
            mat.Elements[5] = child.Elements[3] * parent.Elements[2] + child.Elements[4] * parent.Elements[5] + child.Elements[5];
            return mat;
        }

        public static Matrix operator * (Matrix a, Matrix b)
        {
            return Matrix.Multiply(a, b);
        }

        /// <summary>
        /// Returns the determinant of the matrix.
        /// If non-zero, the matrix is invertible.
        /// </summary>
        public double Determinant()
        {
            return Elements[0] * Elements[4] - Elements[1] * Elements[3];
        }

        /// <summary>
        /// Finds out if the matrix is invertible or not.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this matrix is invertible; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInvertible()
        {
            return Determinant() != 0;
        }

        /// <summary>
        /// Creates an inverted matrix that transforms coordinates the opposite way.
        /// </summary>
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

        /// <summary>
        /// Finds out whether the matrix is a translating matrix or not.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this matrix is translating; otherwise, <c>false</c>.
        /// </returns>
        public bool IsTranslating()
        {
            return Elements[0] == 1 
				&& Elements[1] == 0 
				&& Elements[3] == 0 
				&& Elements[4] == 1;
        }

    }
}

