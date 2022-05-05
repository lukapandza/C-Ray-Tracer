using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    struct comps
    {
        public double t;
        public bool inside;
        public Object obj;
        public Tuple.Point point;
        public Tuple.Vector eyev;
        public Tuple.Vector normalv;
        public Tuple.Point overPoint;
        public Tuple.Vector reflectv;
        public Tuple.Point underPoint;
        public double n1;
        public double n2;
    }

    class Intersection
    {
        public double t;
        public Object obj;
        public Ray ray;

        public Intersection(double t, Object o, Ray raymond)
        {
            this.t = t;
            this.obj = o;
            this.ray = raymond;
        }

        public static comps prepare_computations(Intersection i, Ray raymond, Intersection[] xs)
        {
            comps output = new comps();

            var container = new List<Object>();
            Intersection hit = otherHit(i, xs); //had to write otherHit to exclude the old intersections from the array

            for (int j = 0; j < xs.Length; j++)
            {
                if (xs[j] == hit)
                {
                    if(container.Count == 0) output.n1 = 1;
                    else output.n1 = container[container.Count - 1].material.indexOfRefraction;
                }

                if(container.Any(p => p == xs[j].obj)) container.RemoveAt(container.FindIndex(p => p == xs[j].obj));
                else container.Add(xs[j].obj);

                if(xs[j] == hit)
                {
                    if(container.Count == 0) output.n2 = 1;
                    else output.n2 = container[container.Count - 1].material.indexOfRefraction;
                    j = xs.Length;
                }
            }

            output.t = i.t;
            output.obj = i.obj;
            output.point = Ray.position(raymond, output.t);
            output.eyev = -1 * raymond.direction;
            output.normalv = i.obj.normalAt(output.point);

            if (Tuple.Vector.dotProduct(output.normalv, output.eyev) < 0)
            {
                output.inside = true;
                output.normalv = -1 * output.normalv;
            }
            else output.inside = false;

            output.overPoint = output.point + output.normalv * Constants.EPSILON;
            output.underPoint = output.point - output.normalv * Constants.EPSILON;

            output.reflectv = Tuple.Vector.reflect(raymond.direction, output.normalv);

            return output;
        }

        public static Color shade_hit(World world, comps comps, int remaining)
        {
            Color output = new Color(0, 0, 0);
            
            for (int i = 0; i < world.lights.Length; i++)
            {
                bool shadowed = World.is_shadowed(world, world.lights[i], comps.overPoint);
                output = output + Light.lighting(comps.obj.material, world.lights[i], comps.overPoint, comps.eyev, comps.normalv, shadowed, comps.obj);
            }

            Color reflectedColor = Ray.reflectedColor(world, comps, remaining);
            Color refractedColor = Ray.refractedColor(world, comps, remaining);

            if (comps.obj.material.reflectivity > 0 && comps.obj.material.transparency > 0)
            {
                double reflectance = Ray.schlick(comps);
                return output + reflectedColor * reflectance + refractedColor * (1 - reflectance);
            }
            else return output + reflectedColor + refractedColor;
        }

        public static Intersection hit(Intersection[] ints)
        {
            for (int i = 0; i < ints.Length; i++)
            {
                if(ints[i].t >= 0) return ints[i];
            }
            return null;
        }

        public static Intersection otherHit(Intersection inter, Intersection[] xs)
        {
            for (int i = 0; i < xs.Length; i++)
            {
                if (xs[i].t >= 0 && xs[i].t >= inter.t) return xs[i];
            }
            return null;
        }
    }
}
