using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class Matrix
    {
        public double[,] values;
        public int width;
        public int height;
        public bool invertible
        {
            get
            {
                if (getDeterminant(this) == 0) return false;
                else return true;
            }
        }

        public Matrix(double[,] array)
        {
            this.values = array;
            this.height = array.GetLength(0);
            this.width = array.GetLength(1);
        }

        public Matrix(int w, int h)
        {
            double[,] zeros = new double[h, w];
            for (int i = 0; i < zeros.GetLength(0); i++)
            {
                for (int j = 0; j < zeros.GetLength(1); j++)
                {
                    zeros[i, j] = 0;
                }
            }
            this.values = zeros;
            this.height = h;
            this.width = w;
        }

        public static bool operator ==(Matrix m1, Matrix m2)
        {
            for (int i = 0; i < m1.height; i++)
            {
                for (int j = 0; j < m2.width; j++)
                {
                    if (Math.Abs(m1.values[i, j] - m2.values[i, j]) > Constants.EPSILON) return false;
                }
            }
            return true;
        }

        public static bool operator !=(Matrix m1, Matrix m2)
        {
            for (int i = 0; i < m1.height; i++)
            {
                for (int j = 0; j < m2.width; j++)
                {
                    if (Math.Abs(m1.values[i, j] - m2.values[i, j]) > Constants.EPSILON) return true;
                }
            }
            return false;
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            double[,] vals = new double[m1.height, m1.width];
            for (int i = 0; i < vals.GetLength(0); i++)
            {
                for (int j = 0; j < vals.GetLength(1); j++)
                {
                    vals[i, j] = m1.values[i, j] + m2.values[i, j];
                }
            }
            return new Matrix(vals);
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            Matrix[] arr = new Matrix[m1.width];
            for (int n = 0; n < arr.Length; n++)
            {
                arr[n] = new Matrix(m1.height, m1.width);
                for (int i = 0; i < arr.Length; i++)
                {
                    for (int j = 0; j < arr.Length; j++)
                    {
                        arr[n].values[i, j] = m1.values[i, n] * m2.values[n, j];
                    }
                }
            }
            Matrix zero = new Matrix(m1.width, m1.height);
            for (int i = 0; i < arr.Length; i++)
            {
                zero = zero + arr[i];
            }
            return zero;
        }

        public static Tuple operator *(Matrix m, Tuple t)
        {
            double x = (m.values[0, 0] * t.x + m.values[0, 1] * t.y + m.values[0, 2] * t.z + m.values[0, 3] * t.w);
            double y = (m.values[1, 0] * t.x + m.values[1, 1] * t.y + m.values[1, 2] * t.z + m.values[1, 3] * t.w);
            double z = (m.values[2, 0] * t.x + m.values[2, 1] * t.y + m.values[2, 2] * t.z + m.values[2, 3] * t.w);
            double w = (m.values[3, 0] * t.x + m.values[3, 1] * t.y + m.values[3, 2] * t.z + m.values[3, 3] * t.w);
            return new Tuple(x, y, z, w);
        }

        public static Matrix identity(int h, int w)
        {
            double[,] arr = new double[h, w];
            for(int i = 0; i < arr.GetLength(0); i++)
            {
                for(int j = 0; j < arr.GetLength(1); j++)
                {
                    if(i == j) arr[i, j] = 1;
                    else arr[i, j] = 0;
                }
            }
            return new Matrix(arr);
        }

        public static Matrix getSubmatrix(Matrix inMat, int I, int J)
        {
            double[,] outVal = new double[inMat.height - 1, inMat.width - 1];
            int k = 0;
            int l = 0;
            bool up = false;
            for(int i = 0; i < inMat.height; i++)
            {
                for(int j = 0; j < inMat.width; j++)
                {
                    if(i != I && j != J)
                    {
                        outVal[k, l] = inMat.values[i, j];
                        l++;
                        up = true;
                    }
                }
                if (up)
                {
                    l = 0;
                    k++;
                    up = false;
                }
            }
            return new Matrix(outVal);
        }

        public static double getDeterminant(Matrix mat)
        {
            if (mat.height == 2 && mat.width == 2)
            {
                return (mat.values[0, 0] * mat.values[1, 1]) - (mat.values[0, 1] * mat.values[1, 0]);
            }
            else if(mat.height == 3 && mat.width == 3)
            {
                return mat.values[0, 0] * (mat.values[1, 1] * mat.values[2, 2] - mat.values[1, 2] * mat.values[2, 1])
                        - mat.values[0, 1] * (mat.values[1, 0] * mat.values[2, 2] - mat.values[1, 2] * mat.values[2, 0])
                        + mat.values[0, 2] * (mat.values[1, 0] * mat.values[2, 1] - mat.values[1, 1] * mat.values[2, 0]);
            }
            else if(mat.height == 4 && mat.width == 4)
            {
                return mat.values[0, 0] * (mat.values[1, 1] * mat.values[2, 2] - mat.values[1, 2] * mat.values[2, 1])
                        - mat.values[0, 1] * (mat.values[1, 0] * mat.values[2, 2] - mat.values[1, 2] * mat.values[2, 0])
                        + mat.values[0, 2] * (mat.values[1, 0] * mat.values[2, 1] - mat.values[1, 1] * mat.values[2, 0]);
            }
            else
            {
                throw new System.ArgumentException("Illegal matrix size", "original");
            }
        }

        public static double getMinor(Matrix inMat, int I, int J)
        {
            return getDeterminant(getSubmatrix(inMat, I, J));
        }

        public static double getCofactor(Matrix inMat, int i, int j)
        {
            return Math.Pow(-1, i + j) * getMinor(inMat, i, j);
        }

        public static Matrix getTranspose(Matrix M)
        {
            double[,] arrM = new double[M.width, M.height];
            for (int i = 0; i < M.height; i++)
            {
                for (int j = 0; j < M.width; j++)
                {
                    arrM[i, j] = M.values[j, i];
                }
            }
            return new Matrix(arrM);
        }

        public static Matrix getInverse(Matrix M)
        {
            M = getTranspose(M);
            double[,] arrM = new double[M.height, M.width];
            double det = getDeterminant(M);
            for (int i = 0; i < M.height; i++)
            {
                for (int j = 0; j < M.width; j++)
                {
                    arrM[i, j] = Math.Pow(-1, i + j) * getMinor(M, i, j) * (1 / det);
                }
            }
            return new Matrix(arrM);
        }

        public static Matrix getTranslationMatrix(double x, double y, double z)
        {
            Matrix outM = Matrix.identity(4, 4);
            outM.values[0, 3] = x;
            outM.values[1, 3] = y;
            outM.values[2, 3] = z;
            return outM;
        }

        public static Matrix getScalingMatrix(double x, double y, double z)
        {
            Matrix outM = Matrix.identity(4, 4);
            outM.values[0, 0] = x;
            outM.values[1, 1] = y;
            outM.values[2, 2] = z;
            return outM;
        }

        public static Matrix rotationX(double r)
        {
            Matrix outMat = new Matrix(new double[4, 4] { { 1, 0, 0, 0 }, { 0, Math.Cos(r), -Math.Sin(r), 0 }, { 0, Math.Sin(r), Math.Cos(r), 0 }, { 0, 0, 0, 1 } });
            return outMat;
        }

        public static Matrix rotationY(double r)
        {
            Matrix outMat = new Matrix(new double[4, 4] { { Math.Cos(r), 0, Math.Sin(r), 0 }, { 0, 1, 0, 0 }, { -Math.Sin(r), 0, Math.Cos(r), 0 }, { 0, 0, 0, 1 } });
            return outMat;
        }

        public static Matrix rotationZ(double r)
        {
            Matrix outMat = new Matrix(new double[4, 4] { { Math.Cos(r), -Math.Sin(r), 0, 0 }, { Math.Sin(r), Math.Cos(r), 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } });
            return outMat;
        }

        public static Matrix view_transformation(Tuple.Point from, Tuple.Point to, Tuple.Vector up)
        {
            Tuple.Vector forward = (to - from).normalize();
            Tuple.Vector upn = up.normalize();
            Tuple.Vector left = Tuple.Vector.crossProduct(forward, upn);
            Tuple.Vector trueUp = Tuple.Vector.crossProduct(left, forward);

            Matrix orientation = new Matrix(new double[4, 4] { { left.x, left.y, left.z, 0 }, { trueUp.x, trueUp.y, trueUp.z, 0 }, { -forward.x, -forward.y, -forward.z, 0 }, { 0, 0, 0, 1 } });
            return orientation * Matrix.getTranslationMatrix(-from.x, -from.y, -from.z);

        }
    }
}
