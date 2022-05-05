using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class Color
    {
        public double r, g, b;

        public Color(double R, double G, double B)
        {
            this.r = R;
            this.g = G;
            this.b = B;
        }

        public int intRed()
        {
            int output;
            if (this.r > 1.0) output = 255;
            else output = Convert.ToInt32(this.r * 255);
            return output;
        }

        public int intBlue()
        {
            int output;
            if (this.b > 1.0) output = 255;
            else output = Convert.ToInt32(this.b * 255);
            return output;
        }

        public int intGreen()
        {
            int output;
            if (this.g > 1.0) output = 255;
            else output = Convert.ToInt32(this.g * 255);
            return output;
        }

        public static Color operator +(Color c1, Color c2)
        {
            double solutionR = c1.r + c2.r;
            double solutionG = c1.g + c2.g;
            double solutionB = c1.b + c2.b;
            Color solution = new Color(solutionR, solutionG, solutionB);
            return solution;
        }

        public static Color operator -(Color c1, Color c2)
        {
            double solutionR = c1.r - c2.r;
            double solutionG = c1.g - c2.g;
            double solutionB = c1.b - c2.b;
            Color solution = new Color(solutionR, solutionG, solutionB);
            return solution;
        }

        public static Color operator *(Color c1, double k)
        {
            double solutionR = c1.r * k;
            double solutionG = c1.g * k;
            double solutionB = c1.b * k;
            Color solution = new Color(solutionR, solutionG, solutionB);
            return solution;
        }

        public static Color operator *(double k, Color c1)
        {
            return c1 * k;
        }

        public static Color operator *(Color c1, Color c2)
        {
            double solutionR = c1.r * c2.r;
            double solutionG = c1.g * c2.g;
            double solutionB = c1.b * c2.b;
            Color solution = new Color(solutionR, solutionG, solutionB);
            return solution;
        }

        public static bool operator ==(Color c1, Color c2)
        {
            bool cX = Math.Abs(c1.r - c2.r) < Constants.EPSILON;
            bool cY = Math.Abs(c1.g - c2.g) < Constants.EPSILON;
            bool cZ = Math.Abs(c1.b - c2.b) < Constants.EPSILON;
            if (cX && cY && cZ) return true;
            else return false;
        }

        public static bool operator !=(Color c1, Color c2)
        {
            bool cX = Math.Abs(c1.r - c2.r) >= Constants.EPSILON;
            bool cY = Math.Abs(c1.g - c2.g) >= Constants.EPSILON;
            bool cZ = Math.Abs(c1.b - c2.b) >= Constants.EPSILON;
            if (cX && cY && cZ) return true;
            else return false;
        }
    }
}
