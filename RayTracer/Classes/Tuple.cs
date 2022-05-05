using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    
    class Tuple
    {
        public double x, y, z, w;

        public bool isPoint
        {
            get
            {
                return w == 1.0;
            }
        }

        public bool isVector
        {
            get
            {
                return !isPoint;
            }
        }

        public Tuple(double X, double Y, double Z, double W)
        {
            this.x = X;
            this.y = Y;
            this.z = Z;
            this.w = W;
        }

        public class Point : Tuple
        {
            public Point(double p1, double p2, double p3) : base(p1, p2, p3, 1.0)
            {
                this.x = p1;
                this.y = p2;
                this.z = p3;
                this.w = 1.0;
            }
            public static Vector operator -(Point p1, Point p2)
            {
                double solutionX = p1.x - p2.x;
                double solutionY = p1.y - p2.y;
                double solutionZ = p1.z - p2.z;
                Vector solution = new Vector(solutionX, solutionY, solutionZ);
                return solution;
            }

            public static Point operator -(Point p1, Vector v1)
            {
                double solutionX = p1.x - v1.x;
                double solutionY = p1.y - v1.y;
                double solutionZ = p1.z - v1.z;
                Point solution = new Point(solutionX, solutionY, solutionZ);
                return solution;
            }

            public static Point operator +(Point p1, Vector v1)
            {
                Point solution = new Point(p1.x + v1.x, p1.y + v1.y, p1.z + v1.z);
                return solution;
            }

        }

        public class Vector : Tuple
        {
            public Vector(double v1, double v2, double v3) : base(v1, v2, v3, 0.0)
            {
                this.x = v1;
                this.y = v2;
                this.z = v3;
                this.w = 0.0;
            }

            public double magnitude()
            {
                return Math.Sqrt((this.x * this.x) + (this.y * this.y) + (this.z * this.z));
            }

            public Vector normalize()
            {
                double mag = this.magnitude();
                return new Vector(this.x / mag, this.y / mag, this.z / mag);
            }

            public static double dotProduct(Vector v1, Vector v2)
            {
                return ((v1.x * v2.x) + (v1.y * v2.y) + (v1.z * v2.z));
            }

            public static Vector crossProduct(Vector v1, Vector v2)
            {
                return new Vector((v1.y * v2.z) - (v1.z * v2.y), (v1.z * v2.x) - (v1.x * v2.z), (v1.x * v2.y) - (v1.y * v2.x));
            }

            public static Vector reflect(Vector inVec, Vector normal)
            {
                return (inVec - (normal * 2 * dotProduct(inVec, normal)));
            }

            public static Vector operator -(Vector v1, Vector v2)
            {
                double solutionX = v1.x - v2.x;
                double solutionY = v1.y - v2.y;
                double solutionZ = v1.z - v2.z;
                Vector solution = new Vector(solutionX, solutionY, solutionZ);
                return solution;
            }

            public static Vector operator *(Vector v1, double c)
            {
                Vector v2 = new Vector(v1.x * c, v1.y * c, v1.z * c);
                return v2;
            }
            public static Vector operator *(double c, Vector v1)
            {
                return v1 * c;
            }

        }

        public static Tuple operator +(Tuple t1, Tuple t2)
        {
            double solutionX = t1.x + t2.x;
            double solutionY = t1.y + t2.y;
            double solutionZ = t1.z + t2.z;
            double solutionW = t1.w + t2.w;
            Tuple solution = new Tuple(solutionX, solutionY, solutionZ, solutionW);
            return solution;
        }

        public static Tuple operator -(Tuple t1)
        {
            double solutionX = 0 - t1.x;
            double solutionY = 0 - t1.y;
            double solutionZ = 0 - t1.z;
            double solutionW = 0 - t1.w;
            Tuple solution = new Tuple(solutionX, solutionY, solutionZ, solutionW);
            return solution;
        }

        public static Tuple operator *(double c, Tuple t1)
        {
            double solutionX = c * t1.x;
            double solutionY = c * t1.y;
            double solutionZ = c * t1.z;
            double solutionW = c * t1.w;
            Tuple solution = new Tuple(solutionX, solutionY, solutionZ, solutionW);
            return solution;
        }

        public static Tuple operator *(Tuple t1, double c)
        {
            return c * t1;
        }

        public static Tuple operator /(Tuple tuple1, double c)
        {
            double solutionX = tuple1.x / c;
            double solutionY = tuple1.y / c;
            double solutionZ = tuple1.z / c;
            double solutionW = tuple1.w / c;
            Tuple solution = new Tuple(solutionX, solutionY, solutionZ, solutionW);
            return solution;
        }

        public static bool operator ==(Tuple t1, Tuple t2)
        {
            bool vX = Math.Abs(t1.x - t2.x) < Constants.EPSILON;
            bool vY = Math.Abs(t1.y - t2.y) < Constants.EPSILON;
            bool vZ = Math.Abs(t1.z - t2.z) < Constants.EPSILON;
            bool vW = Math.Abs(t1.w - t2.w) < Constants.EPSILON;
            if (vX && vY && vZ && vW)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Tuple t1, Tuple t2)
        {
            bool vX = Math.Abs(t1.x - t2.x) < Constants.EPSILON;
            bool vY = Math.Abs(t1.y - t2.y) < Constants.EPSILON;
            bool vZ = Math.Abs(t1.z - t2.z) < Constants.EPSILON;
            bool vW = Math.Abs(t1.w - t2.w) < Constants.EPSILON;
            if (vX || vY || vZ || vW)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
