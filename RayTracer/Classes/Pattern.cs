using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class Pattern
    {
        private Matrix pTransform;
        public Matrix inverse_transform;
        public Matrix transform
        {
            get
            {
                return pTransform;
            }
            set
            {
                inverse_transform = Matrix.getInverse(value);
                pTransform = value;
            }
        }
        public Color[] colors;

        public Pattern(Matrix transform, Color[] colors)
        {
            this.transform = transform;
            this.colors = colors;
        }

        public static Color stripes(Pattern pat, Object obj, Tuple.Point p)
        {
            Tuple.Point pO = new Tuple.Point((obj.inverse_transform * p).x, (obj.inverse_transform * p).y, (obj.inverse_transform * p).z);
            Tuple.Point pL = new Tuple.Point((pat.inverse_transform * pO).x, (pat.inverse_transform * pO).y, (pat.inverse_transform * pO).z);

            if(Convert.ToInt32(pL.x) % 2 == 0) return pat.colors[0];
            else return pat.colors[1];
        }

        public static Color gradient(Pattern pat, Object obj, Tuple.Point p)
        {
            Tuple.Point pO = new Tuple.Point((obj.inverse_transform * p).x, (obj.inverse_transform * p).y, (obj.inverse_transform * p).z);
            Tuple.Point pL = new Tuple.Point((pat.inverse_transform * pO).x, (pat.inverse_transform * pO).y, (pat.inverse_transform * pO).z);

            Color gradCol = pat.colors[1] - pat.colors[0];
            double part = pL.x - Convert.ToInt32(pL.x);
            return pat.colors[0] + gradCol * part;
        }

        public static Color ring(Pattern pat, Object obj, Tuple.Point p)
        {
            Tuple.Point pO = new Tuple.Point((obj.inverse_transform * p).x, (obj.inverse_transform * p).y, (obj.inverse_transform * p).z);
            Tuple.Point pL = new Tuple.Point((pat.inverse_transform * pO).x, (pat.inverse_transform * pO).y, (pat.inverse_transform * pO).z);

            if((Convert.ToInt32(Math.Sqrt(Math.Pow(pL.x, 2) + Math.Pow(pL.z, 2))) % 2) == 0) return pat.colors[0];
            else return pat.colors[1];
        }

        public static Color checkered(Pattern pat, Object obj, Tuple.Point p)
        {
            Tuple.Point pO = new Tuple.Point((obj.inverse_transform * p).x, (obj.inverse_transform * p).y, (obj.inverse_transform * p).z);
            Tuple.Point pL = new Tuple.Point((pat.inverse_transform * pO).x, (pat.inverse_transform * pO).y, (pat.inverse_transform * pO).z);

            if(((Math.Abs(Convert.ToInt32(pL.x)) + Math.Abs(Convert.ToInt32(pL.y)) + Math.Abs(Convert.ToInt32(pL.z))) % 2) == 0) return pat.colors[0];
            else return pat.colors[1];
        }

        public static Color squareRing(Pattern pat, Object obj, Tuple.Point p)
        {
            Tuple.Point pO = new Tuple.Point((obj.inverse_transform * p).x, (obj.inverse_transform * p).y, (obj.inverse_transform * p).z);
            Tuple.Point pL = new Tuple.Point((pat.inverse_transform * pO).x, (pat.inverse_transform * pO).y, (pat.inverse_transform * pO).z);

            if ((Convert.ToInt32(Math.Abs(pL.x) + Math.Abs(pL.y) + Math.Abs(pL.z)) % 2) == 0) return pat.colors[0];
            else return pat.colors[1];
        }
    }
}
