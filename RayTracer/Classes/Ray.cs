using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    class Ray
    {
        public Tuple.Point origin;
        public Tuple.Vector direction;

        public Ray(Tuple.Point p, Tuple.Vector v)
        {
            this.origin = p;
            this.direction = v;
        }

        public static Tuple.Point position(Ray ray, double c)
        {
            Tuple.Vector vec = ray.direction * c;
            Tuple.Point solution = ray.origin + vec;
            return solution;
        }

        public static Intersection[] intersect(World w, Ray ray)
        {
            List<Intersection> outXs = new List<Intersection>();
            for(int i = 0; i < w.objects.Length; i++)
            {
                Intersection[] tmp = w.objects[i].intersectWith(ray);
                for(int j = 0; j < tmp.Length; j++)
                {
                    outXs.Add(tmp[j]);
                }
            }
            List<Intersection> sorted = outXs.OrderBy(o => o.t).ToList();
            return sorted.ToArray();
        }

        public static Tuple.Point pointAtT(Ray ray, double t)
        {
            return ray.origin + (ray.direction * t);
        }

        public static Ray transform(Ray inRay, Matrix transformMatrix)
        {
            Tuple tO = transformMatrix * inRay.origin;
            Tuple tD = transformMatrix * inRay.direction;
            return new Ray(new Tuple.Point(tO.x, tO.y, tO.z), new Tuple.Vector(tD.x, tD.y, tD.z));
        }

        public static Color color_at(World w, Ray raymond, int remaining)
        {
            Intersection[] inters = Ray.intersect(w, raymond);
            Intersection hit = Intersection.hit(inters);
            if (hit == null) return new Color(0, 0, 0);
            else
            {
                
                comps c = Intersection.prepare_computations(hit, raymond, new Intersection[1] { hit});
                return Intersection.shade_hit(w, c, remaining);
            }
        }

        public static Color reflectedColor(World world, comps comps, int remaining = 5)
        {
            if (remaining == 0) return new Color(0, 0, 0);
            if (comps.obj.material.reflectivity == 0) return new Color(0, 0, 0);
            Ray reflectRay = new Ray(comps.overPoint, comps.reflectv);
            Color col = Ray.color_at(world, reflectRay, remaining - 1);
            return col * comps.obj.material.reflectivity;
        }

        public static Color refractedColor(World w, comps comps, int remaining)
        {
            double nRatio = comps.n1 / comps.n2;
            double cosI = Tuple.Vector.dotProduct(comps.eyev, comps.normalv);
            double sin2I = nRatio * nRatio * (1 - cosI * cosI);

            if (comps.obj.material.transparency == 0 || remaining == 0 || sin2I > 1) return new Color(0, 0, 0);
            else
            {
                double cosT = Math.Sqrt(1 - sin2I);
                Tuple.Vector dir = comps.normalv * (nRatio * cosI - cosT) - comps.eyev * nRatio;
                Ray refractionRay = new Ray(comps.underPoint, dir);
                return color_at(w, refractionRay, remaining - 1) * comps.obj.material.transparency;
            }
        }

        public static double schlick(comps comps)
        {
            double cos = Tuple.Vector.dotProduct(comps.eyev, comps.normalv);
            if(comps.n1 > comps.n2)
            {
                double n = comps.n1 / comps.n2;
                double sin2T = n * n * (1 - cos * cos);

                if(sin2T > 1) return 1;

                double cosT = Math.Sqrt(1 - sin2T);
                cos = cosT;
            }
            double r0 = ((comps.n1 - comps.n2) / (comps.n1 + comps.n2)) * ((comps.n1 - comps.n2) / (comps.n1 + comps.n2));
            return r0 + (1 - r0) * Math.Pow((1 - cos), 5);
        }
    }
}
